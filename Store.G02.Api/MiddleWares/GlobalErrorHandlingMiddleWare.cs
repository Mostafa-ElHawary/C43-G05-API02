using Domain.Exceptions;
using Shared.ErrorModels;

namespace Store.G02.Api.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare 
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;
        public GlobalErrorHandlingMiddleWare(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode  == StatusCodes.Status404NotFound)
                {
                    await HandleNotFoundEndPointAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wong !");
                await HandleExcpetionAsync(context, ex);

            }
        }

        private static async Task HandleExcpetionAsync(HttpContext context, Exception ex)
        {
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                //StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message,
            };

            response.StatusCode = ex switch
            {
                NotFoundExcpetion => StatusCodes.Status404NotFound,
                BadRequestExceptions => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,

                ValidationExceptions => HandleValidationExceptionAsync((ValidationExceptions)ex ,response),
                _ => StatusCodes.Status500InternalServerError,
            };
            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"EndPoint {context.Request.Path} Not Found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }

        private static int HandleValidationExceptionAsync(ValidationExceptions ex , ErrorDetails response)
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
