using HospitalManagementSystem.Service.StatisticsService.NursingStaffService.StaffStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.NursingStaffService
{
    [Route("api/statistics/staff")]
    [ApiController]
    public class StaffStatisticsController : ControllerBase
    {
        private readonly IStaffStatService _staffStatService;
        private readonly ILogger<StaffStatisticsController> _logger;

        public StaffStatisticsController(
            IStaffStatService staffStatService,
            ILogger<StaffStatisticsController> logger)
        {
            _staffStatService = staffStatService;
            _logger = logger;
        }
        [HttpGet("role-distribution")]
        public async Task<IActionResult> GetStaffRoleDistribution()
        {
            _logger.LogInformation("Request received to get staff distribution by role.");
            var response = await _staffStatService.GetStaffDistributionByRoleAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("top-clinics")]
        public async Task<IActionResult> GetTopClinicsByStaffCount()
        {
            _logger.LogInformation("Request received to get top clinics by staff count.");
            var response = await _staffStatService.GetTopClinicsByStaffCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
