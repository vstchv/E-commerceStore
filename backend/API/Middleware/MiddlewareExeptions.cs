using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class MiddlewareExeptions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExeptions> _logger;
        private readonly IHostEnvironment _environment;
        public MiddlewareExeptions(RequestDelegate next, ILogger<MiddlewareExeptions> logger, IHostEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
            _next = next;

        }


        // creates better handling error for internal server bugs 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = new ProblemDetails
                {
                    Status = 500,
                    Detail = _environment.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = ex.Message
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);

            }
        }

    }
}
