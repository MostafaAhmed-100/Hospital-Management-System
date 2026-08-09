using HospitalManagementSystem.Service.StatisticsService.InpatientService.BedService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.InpatientControllers
{
    [Route("api/statistics/beds")]
    [ApiController]
    public class BedStatisticsController : ControllerBase
    {
        private readonly IBedStatService _bedStatService;
        private readonly ILogger<BedStatisticsController> _logger;

        public BedStatisticsController(
            IBedStatService bedStatService,
            ILogger<BedStatisticsController> logger)
        {
            _bedStatService = bedStatService;
            _logger = logger;
        }

        [HttpGet("available-count")]
        public async Task<IActionResult> GetAvailableBedsCount()
        {
            _logger.LogInformation("Request received to get available beds count.");
            var response = await _bedStatService.GetAvailableBedsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("status-distribution")]
        public async Task<IActionResult> GetBedsStatusDistribution()
        {
            _logger.LogInformation("Request received to get beds status distribution.");
            var response = await _bedStatService.GetBedsDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
