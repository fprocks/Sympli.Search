using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sympli.Search.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");

                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var response = context.Response;

            var stackTrace = String.Empty;
            if (env.IsEnvironment("Development"))
                stackTrace = ex.StackTrace;

            // Note: handle custom exceptions here

            var result = JsonSerializer.Serialize(new { error = ex?.Message, stackTrace });

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return response.WriteAsync(result);
        }
    }
}
