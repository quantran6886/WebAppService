namespace WebAppService.Middlewares
{
    public class InvalidDomainMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _allowedDomains;
        public InvalidDomainMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var domains = configuration.GetSection("AllowedDomains").Get<string[]>() ?? Array.Empty<string>();
            _allowedDomains = new HashSet<string>(domains, StringComparer.OrdinalIgnoreCase);
        }

        public async Task Invoke(HttpContext context)
        {
            var currentHost = context.Request.Host.Host;

            if (!_allowedDomains.Contains(currentHost))
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 - Domain không hợp lệ.");
                return;
            }

            await _next(context);
        }
    }
}
