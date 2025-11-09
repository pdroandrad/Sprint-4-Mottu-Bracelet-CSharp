using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(HealthCheckService healthCheckService, ILogger<HealthController> logger)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            var result = new
            {
                status = report.Status.ToString(),
                totalDuration = report.TotalDuration.TotalMilliseconds + "ms",
                entries = report.Entries.ToDictionary(
                    e => e.Key,
                    e => new
                    {
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        duration = e.Value.Duration.TotalMilliseconds + "ms",
                        data = e.Value.Data.ToDictionary(d => d.Key, d => d.Value?.ToString() ?? string.Empty)
                    }
                )
            };

            _logger.LogInformation("Health check executed. Status: {status}", report.Status.ToString());

            if (report.Status == HealthStatus.Unhealthy)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, result);
            }

            return Ok(result);
        }
    }
}
