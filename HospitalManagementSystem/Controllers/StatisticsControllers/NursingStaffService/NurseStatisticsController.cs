using HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.NursingStaffService
{
    [Route("api/statistics/nurses")]
    [ApiController]
    public class NurseStatisticsController : ControllerBase
    {
        private readonly INurseStatService _nurseStatService;
        private readonly ILogger<NurseStatisticsController> _logger;

        public NurseStatisticsController(
            INurseStatService nurseStatService,
            ILogger<NurseStatisticsController> logger)
        {
            _nurseStatService = nurseStatService;
            _logger = logger;
        }

        [HttpGet("shift-distribution")]
        public async Task<IActionResult> GetNursesShiftDistribution()
        {
            _logger.LogInformation("Request received to get nurses distribution by shift.");
            var response = await _nurseStatService.GetNursesDistributionByShiftAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("top-wards")]
        public async Task<IActionResult> GetTopWardSpecializations()
        {
            _logger.LogInformation("Request received to get top ward specializations for nurses.");
            var response = await _nurseStatService.GetTopWardSpecializationsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
