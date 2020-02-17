using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Middleware;

namespace Extensions
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app, int number)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            UseMiddlewareCore(app, number);
            return app;
        }

        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app, PathString path, int number)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            UseMiddlewareCore(app, path, number);
            return app;
        }

        private static void UseMiddlewareCore(IApplicationBuilder builder, int number)
        {
            switch (number)
            {
                case 0:
                    builder.UseMiddleware<AuthorizeMiddleware>();
                    break;
            }
        }

        private static void UseMiddlewareCore(IApplicationBuilder builder, PathString path, int number)
        {
            switch (number)
            {
                case 0:
                    builder.Map(path, mapBuilder => mapBuilder.UseMiddleware<AuthorizeMiddleware>());
                    break;  
            }
        }
    }
}