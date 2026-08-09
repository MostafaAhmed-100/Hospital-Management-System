using HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryTeamStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.SurgeryControllers
{
    [Route("api/statistics/surgery-teams")]
    [ApiController]
    public class SurgeryTeamStatisticsController : ControllerBase
    {
        private readonly ISurgeryTeamStatService _surgeryTeamStatService;
        private readonly ILogger<SurgeryTeamStatisticsController> _logger;

        public SurgeryTeamStatisticsController(
            ISurgeryTeamStatService surgeryTeamStatService,
            ILogger<SurgeryTeamStatisticsController> logger)
        {
            _surgeryTeamStatService = surgeryTeamStatService;
            _logger = logger;
        }
        [HttpGet("top-active-staff")]
        public async Task<IActionResult> GetTopActiveSurgeryStaff()
        {
            _logger.LogInformation("Request received to get top active surgery staff.");
            var response = await _surgeryTeamStatService.GetTopActiveSurgeryStaffAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("role-distribution")]
        public async Task<IActionResult> GetSurgeryRoleDistribution()
        {
            _logger.LogInformation("Request received to get surgery role distribution.");
            var response = await _surgeryTeamStatService.GetSurgeryRoleDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
