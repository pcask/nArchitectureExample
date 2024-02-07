
using System.Diagnostics;
using System.Net.Mime;
using System.Text.Json;
using Business.Abstracts;
using Business.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BookStore.WebApi.Middlewares
{
    public class CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
    {
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message;
            try
            {
                message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
                logger.Log(message);

                await next(context); // next.Invoke(context); aynısı.

                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                logger.Log(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            // Mevcut response'ı ezelim.
            context.Response.ContentType = MediaTypeNames.Application.Json;

            short StatusCode = StatusCodes.Status500InternalServerError;
            string expMessage = "Internal Server Error";

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                if (ex is ValidationException)
                {
                    StatusCode = (ex as ValidationException).StatusCode;
                    expMessage = ex.Message;
                    break;
                }
            }

            context.Response.StatusCode = StatusCode;

            string message = "[Error]    HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms. " + "Error Message: " + expMessage;
            logger.Log(message);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var result = JsonSerializer.Serialize(new { error = expMessage }, options);

            await context.Response.WriteAsync(result);
        }
    }

    public static class RegisterCustomExceptionMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}