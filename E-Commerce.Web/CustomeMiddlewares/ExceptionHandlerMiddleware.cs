using ECommerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomeMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);

                if(httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var problem = new ProblemDetails()
                    {
                        Title = "Error while http request - EndPoint not found.",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"Endpoint : {httpcontext.Request.Path} not found.",
                        Instance = httpcontext.Request.Path
                    };
                    await httpcontext.Response.WriteAsJsonAsync(problem);
                }
                
            }
            catch (Exception ex)
            {
                // Logging
                _logger.LogError(ex,"Something went wrong.");

                var problem = new ProblemDetails()
                {
                    Title = "An unexcpected error occured.",
                    Detail = ex.Message,
                    Instance = httpcontext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    },
                };
                httpcontext.Response.StatusCode = problem.Status.Value;

                await httpcontext.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
