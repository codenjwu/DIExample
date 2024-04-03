using System.Globalization;

namespace DIExample.Services
{
    public class OkMiddleware
    {
        private readonly RequestDelegate _next; 

        public OkMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITransientService transientService, IScopedService scopedService)
        {
            context.Items.Add("middleware", $"{transientService.Value()},{scopedService.Value()}");
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }
}
