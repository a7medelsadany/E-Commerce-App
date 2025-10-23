using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.CustomMiddleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"The EndPoint {httpContext.Request.Path} is Not Found"
                    };
                    await httpContext.Response.WriteAsJsonAsync(Response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SomeThing Went Wrong");
                //set status code for Response
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

                //set Content Type For Response
                httpContext.Response.ContentType="application/json";

                //Response Object
                var Response = new ErrorToReturn()
                {
                    StatusCode= httpContext.Response.StatusCode,
                    ErrorMessage= ex.Message
                };

                //Return Response object as Json
                await httpContext.Response.WriteAsJsonAsync(Response);
            }

        }
    }
}
