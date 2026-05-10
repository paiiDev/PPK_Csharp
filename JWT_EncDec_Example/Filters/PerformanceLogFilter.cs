using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace JWT_EncDec_Example.Filters
{
    public class PerformanceLogFilter : IAsyncActionFilter
    {
        private ILogger<PerformanceLogFilter> _logger;
        
        public PerformanceLogFilter(ILogger<PerformanceLogFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            var endpointName = context.ActionDescriptor.DisplayName;

            _logger.LogInformation($"Calling api: {endpointName}");
            var resultContext = await next();


            stopwatch.Stop();
            var timeTaken = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation($"[END] {endpointName} takes {timeTaken} ms");

            if (resultContext.Exception != null)
            {
                _logger.LogInformation($"[Error]: {resultContext.Exception}");
            }


        }
    }
}
