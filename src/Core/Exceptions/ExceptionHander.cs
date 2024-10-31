namespace  GestionInventario.src.Core.Exceptions;
using Newtonsoft.Json;

public static class ExceptionHandler
{
    public static void ConfigureExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp => 
        {
            errorApp.Run(async context =>
            {
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