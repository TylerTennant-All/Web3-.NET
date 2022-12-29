﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221229191517_Bytecode Required")]
    partial class BytecodeRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.SmartContract", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AbiSerialized")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Abi");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bytecode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Chain")
                        .HasColumnType("int");

                    b.Property<string>("ParametersSerialized")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Parameters");

                    b.HasKey("Id");

                    b.ToTable("SmartContract");
                });
#pragma warning restore 612, 618
        }
    }
}
