using HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.TherapistStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PhysiotherapyControllers
{
    [Route("api/statistics/therapists")]
    [ApiController]
    public class TherapistStatisticsController : ControllerBase
    {
        private readonly ITherapistStatService _therapistStatService;
        private readonly ILogger<TherapistStatisticsController> _logger;

        public TherapistStatisticsController(
            ITherapistStatService therapistStatService,
            ILogger<TherapistStatisticsController> logger)
        {
            _therapistStatService = therapistStatService;
            _logger = logger;
        }
        [HttpGet("top-active")]
        public async Task<IActionResult> GetTopActiveTherapists()
        {
            _logger.LogInformation("Request received to get top active therapists.");
            var response = await _therapistStatService.GetTopActiveTherapistsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("specialization-distribution")]
        public async Task<IActionResult> GetTherapistSpecializationDistribution()
        {
            _logger.LogInformation("Request received to get therapist specialization distribution.");
            var response = await _therapistStatService.GetTherapistSpecializationDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
