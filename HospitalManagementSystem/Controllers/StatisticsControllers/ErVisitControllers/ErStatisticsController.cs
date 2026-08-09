using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Service.StatisticsService.EmergencyService;

namespace HospitalManagementSystem.Controllers.Statistics
{
    [Route("api/statistics/emergency")]
    [ApiController]
    public class ErStatisticsController : ControllerBase
    {
        private readonly IErVisitStatService _erVisitStatService;
        private readonly ILogger<ErStatisticsController> _logger;

        public ErStatisticsController(
            IErVisitStatService erVisitStatService,
            ILogger<ErStatisticsController> logger)
        {
            _erVisitStatService = erVisitStatService;
            _logger = logger;
        }

        [HttpGet("top-doctors")]
        public async Task<IActionResult> GetTopDoctorsInEr()
        {
            _logger.LogInformation("Request received to get top doctors in ER.");
            var response = await _erVisitStatService.GetTopDoctorsInErAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("active-count")]
        public async Task<IActionResult> GetActiveErVisitsCount()
        {
            _logger.LogInformation("Request received to get active ER visits count.");
            var response = await _erVisitStatService.GetActiveErVisitsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("triage-distribution")]
        public async Task<IActionResult> GetTriageDistribution()
        {
            _logger.LogInformation("Request received to get ER triage distribution.");
            var response = await _erVisitStatService.GetErVisitsDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}