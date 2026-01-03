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
            }
            catch (Exception ex)
            {
                // Logging
                _logger.LogError(ex,"Something went wrong.");

                // Return custom error response
                httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var problem = new ProblemDetails()
                {
                    Title = "An unexcpected error occured.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = httpcontext.Request.Path
                };
                await httpcontext.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
