using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.DataAccess;
using MiTutor.Models.Utils;
using System.Diagnostics;

namespace MiTutor.Controllers.Utils
{
    [Route("/health")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private static readonly Stopwatch _uptimeStopwatch = Stopwatch.StartNew();
        private readonly DatabaseManager _databaseManager;

        public HealthCheckController(DatabaseManager databaseManager, ILogger<HealthCheckController> logger)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var databaseResponseTime = await _databaseManager.MeasureDatabaseResponseTimeAsync();
                var response = new HealthCheckResponse
                {
                    Status = "ok",
                    Uptime = (long)_uptimeStopwatch.Elapsed.TotalSeconds,
                    Timestamp = DateTime.UtcNow,
                    Dependencies = new Dependencies
                    {
                        Database = new ServiceStatus
                        {
                            Status = databaseResponseTime >= 0 ? "ok" : "error",
                            ResponseTimeMs = databaseResponseTime
                        }
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in HealthCheckController GET method at {Time}", DateTime.UtcNow);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
