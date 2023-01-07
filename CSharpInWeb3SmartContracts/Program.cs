using Application;
using Application.Mappers;
using Application.Modules;
using Application.RegisterServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Models;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Infrastructure;
using Infrastructure.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.IO.Compression;
using System.Text.Json.Serialization;
using WebApi.Extensions.Services;
using WebApi.GraphQL;
using WebApi.HealthChecks;
using WebApi.Utilities;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

#region Cors
IServiceCollection configureCors = builder.Services.ConfigureCors();
#endregion Cors

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())).AddNewtonsoftJson();

builder.Services.AddScoped<IGraphQLClient>(s => new GraphQLHttpClient("https://api.thegraph.com/subgraphs/name/uniswap/uniswap-v3", new NewtonsoftJsonSerializer()));
builder.Services.AddScoped<UniswapV3GraphQL>();

#region AppSettings.json
IConfigurationRoot? configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
#endregion AppSettings.json

#region Serilog Logging

var serilogConfiguration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

string fullPath = Environment.CurrentDirectory + @"\logs.txt";
LoggingLevelSwitch? levelSwitch = new();
levelSwitch.MinimumLevel = LogEventLevel.Error;
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(serilogConfiguration)
                                         //.MinimumLevel.ControlledBy(levelSwitch)
                                         .WriteTo.File(new JsonFormatter(), fullPath, rollingInterval: RollingInterval.Day));

/*.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"),
 new MSSqlServerSinkOptions
 {
     TableName = "Logs",
     SchemaName = "dbo",
     AutoCreateSqlTable = true
 }));*/
#endregion Serilog Logging

#region Database
builder.Services.AddPersistence(builder.Configuration);
#endregion Database

#region Autofac Dependency Injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new ServiceModule()));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new RepositoryModule()));
#endregion Autofac Dependency Injection

#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
#endregion AutoMapper

#region Response Cache Profiles
builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("DefaultCache", new CacheProfile() { Duration = 5 });
});
#endregion Response Cache Profiles

#region Mediatr
builder.Services.RegisterMediatr();
#endregion Mediatr

#region FluentValidation
builder.Services.AddFluentValidation();
#endregion FluentValidation

#region Api Gateway Pattern (Proxy Controller)
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("apiGateway").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
});
#endregion Api Gateway Pattern (Proxy Controller)

#region Rate Limit
builder.Services.AddRateLimiting(builder.Configuration.GetSection("IpRateLimiting"));
#endregion Rate Limit

#region Read User appsettings.json
builder.Services.AddOptions<User>().BindConfiguration("User").ValidateDataAnnotations().ValidateOnStart();
#endregion Read User appsettings.json

#region Health Checks
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
builder.Services.AddHealthChecks().AddCheck<HealthCheck>("Custom Health Checks");
#endregion Health Checks

#region Database Exception Filter
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endregion Database Exception Filter

#region Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

#endregion Response Compression

//Load Controllers dynamically from DLL
/*Assembly? assembly = Assembly.LoadFile(@"C:\Users\Nikos\source\repos\LoadDynamicControllers\LoadDynamicControllers\bin\Debug\net6.0\Test.dll");
if (assembly != null)
{
    builder.Services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
    builder.Services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();
}*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
    options.SchemaFilter<EnumSchemaFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseGlobalExceptionMiddleware();

app.RateLimit();

app.UseCors();

app.UseAuthorization();

app.UseResponseCompression();

app.MapControllers();

app.Run();
