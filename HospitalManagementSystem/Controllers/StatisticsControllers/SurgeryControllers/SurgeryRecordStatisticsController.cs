using HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryRecordStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.SurgeryControllers
{
    [Route("api/statistics/surgery-records")]
    [ApiController]
    public class SurgeryRecordStatisticsController : ControllerBase
    {
        private readonly ISurgeryRecordStatService _surgeryRecordStatService;
        private readonly ILogger<SurgeryRecordStatisticsController> _logger;

        public SurgeryRecordStatisticsController(
            ISurgeryRecordStatService surgeryRecordStatService,
            ILogger<SurgeryRecordStatisticsController> logger)
        {
            _surgeryRecordStatService = surgeryRecordStatService;
            _logger = logger;
        }
        [HttpGet("top-types")]
        public async Task<IActionResult> GetTopSurgeryTypes()
        {
            _logger.LogInformation("Request received to get top surgery types.");
            var response = await _surgeryRecordStatService.GetTopSurgeryTypesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("status-distribution")]
        public async Task<IActionResult> GetSurgeryStatusDistribution()
        {
            _logger.LogInformation("Request received to get surgery status distribution.");
            var response = await _surgeryRecordStatService.GetSurgeryStatusDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
