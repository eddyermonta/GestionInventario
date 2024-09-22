namespace  GestionInventario.Domain.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

public static class ExceptionHandler
{
    public static void ConfigureExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var response = new ErrorResponse
                                    {
                                        StatusCode = StatusCodes.Status500InternalServerError,
                                        Message = "An unexpected error occurred. Please try again later."
                                    };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            });
        });
    }
}


