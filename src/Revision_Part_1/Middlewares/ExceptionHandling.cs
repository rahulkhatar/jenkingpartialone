
using Revision_Part_1.Domain.Exceptions;

namespace Revision_Part_1.Middlewares
{
    public class ExceptionHandling(ILogger<ExceptionHandling> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch (ForbiddenAccessException forbidAccess) 
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidAccess.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong.....");
            }
        }
    }
}
