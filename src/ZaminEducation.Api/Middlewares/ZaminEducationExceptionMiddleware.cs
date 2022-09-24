using ZaminEducation.Service.Exceptions;

namespace ZaminEducation.Api.Middlewares
{
    public class ZaminEducationExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ZaminEducationExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (ZaminEducationException ex)
            {
                await this.HandleException(context, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                await this.HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}
