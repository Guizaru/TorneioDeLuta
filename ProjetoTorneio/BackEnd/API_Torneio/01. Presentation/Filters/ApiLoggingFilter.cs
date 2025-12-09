using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Torneio.Filters
{
    public class ApiLoggingFilter :IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Status code: {context.HttpContext.Response.StatusCode}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Executando aplicação");
            _logger.LogInformation("###############################");
            _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        }
    }
}
