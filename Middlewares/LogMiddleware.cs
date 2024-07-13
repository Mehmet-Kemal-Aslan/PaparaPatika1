using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PaparaPatika.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log here
            System.Console.WriteLine($"{context.Request.Path} isteği alındı.");

            await _next(context);
        }
    }
}
