
using System.Diagnostics;
using System.Net.Mime;
using System.Text.Json;
using Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Core.Middlewares;

public class CustomExceptionMiddleware(RequestDelegate next, Stopwatch stopWatch)
{
    public async Task Invoke(HttpContext context)
    {
        string message;
        try
        {
            message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            //logger.Log(message);

            await next(context); // next.Invoke(context); aynısı.

            stopWatch.Stop();
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + stopWatch.Elapsed.TotalMilliseconds + " ms";
            //logger.Log(message);
        }
        catch (Exception ex)
        {
            stopWatch.Stop();
            await HandleExceptionAsync(context, ex, stopWatch);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch watch)
    {
        // Mevcut response'ı ezelim.
        context.Response.ContentType = MediaTypeNames.Application.Json;

        short StatusCode = StatusCodes.Status500InternalServerError;
        string expMessage = ex.Message; // Canlıda hataya ait mesaj alalen gösterilmeyecek örn: "Internal Server Error";

        if (ex is IPresentableException)
        {
            expMessage = ex.Message;

            StatusCode = (ex as IPresentableException).StatusCode;
        }

        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            expMessage = ex.Message;

            if (ex is IPresentableException)
            {
                StatusCode = (ex as IPresentableException).StatusCode;
                break;
            }
        }

        context.Response.StatusCode = StatusCode;

        string message = "[Error] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms. " + "Error Message: " + expMessage;
        //logger.Log(message);

        var options = new JsonSerializerOptions { WriteIndented = true };
        var result = JsonSerializer.Serialize(new { error = message }, options);

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