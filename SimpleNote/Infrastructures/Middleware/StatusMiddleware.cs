using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SimpleNote.Infrastructures.Middleware
{
    public class StatusMiddleware
    {
        private readonly RequestDelegate _next;

        public StatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Equals("/status"))
            {
                await context.Response.WriteAsync("OK");
            } else
            {
                await _next(context);
            }
        }
    }
}
