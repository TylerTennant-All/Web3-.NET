﻿using WebApi.CustomMiddleware;

namespace WebApi.Utilities
{
    public static class GlobalExceptionMiddleware
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
        }
    }
}
