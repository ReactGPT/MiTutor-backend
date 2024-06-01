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
        private static readonly Stopwatch _uptimeStopwatch = Stopwatch.StartNew();
        private readonly DatabaseManager _databaseManager;

        public HealthCheckController(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
    }
}
