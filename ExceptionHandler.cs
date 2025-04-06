using Microsoft.AspNetCore.Diagnostics;

namespace Art_WebAPI
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
    /// <summary>
    /// custom exception handler that catches exceptions, generates an appropriate response, and writes it to the HTTP response body
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// handle exceptions
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is NotImplementedException)
            {
                    var resp = new ErrorResponse()
                    {
                        StatusCode = StatusCodes.Status501NotImplemented,
                        Message = exception.Message
                    };
                await httpContext.Response.WriteAsJsonAsync(resp, cancellationToken);
            }
            else
            {
                var resp = new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(resp, cancellationToken);
            }
            return true;
        }
    }
}
