using HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseAssignmentStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.NursingStaffService
{
    [Route("api/statistics/nurse-assignments")]
    [ApiController]
    public class NurseAssignmentStatisticsController : ControllerBase
    {
        private readonly INurseAssignmentStatService _assignmentStatService;
        private readonly ILogger<NurseAssignmentStatisticsController> _logger;

        public NurseAssignmentStatisticsController(
            INurseAssignmentStatService assignmentStatService,
            ILogger<NurseAssignmentStatisticsController> logger)
        {
            _assignmentStatService = assignmentStatService;
            _logger = logger;
        }
        [HttpGet("top-nurses")]
        public async Task<IActionResult> GetTopAssignedNurses()
        {
            _logger.LogInformation("Request received to get top assigned nurses.");
            var response = await _assignmentStatService.GetTopAssignedNursesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("shift-distribution")]
        public async Task<IActionResult> GetAssignmentsShiftDistribution()
        {
            _logger.LogInformation("Request received to get assignments distribution by shift.");
            var response = await _assignmentStatService.GetAssignmentsDistributionByShiftAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
