using HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.PhysioSessionStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PhysiotherapyControllers
{
    [Route("api/statistics/physio-sessions")]
    [ApiController]
    public class PhysioSessionStatisticsController : ControllerBase
    {
        private readonly IPhysioSessionStatService _physioSessionStatService;
        private readonly ILogger<PhysioSessionStatisticsController> _logger;

        public PhysioSessionStatisticsController(
            IPhysioSessionStatService physioSessionStatService,
            ILogger<PhysioSessionStatisticsController> logger)
        {
            _physioSessionStatService = physioSessionStatService;
            _logger = logger;
        }
        [HttpGet("top-therapy-types")]
        public async Task<IActionResult> GetTopTherapyTypes()
        {
            _logger.LogInformation("Request received to get top therapy types.");
            var response = await _physioSessionStatService.GetTopTherapyTypesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("today-count")]
        public async Task<IActionResult> GetTodayPhysioSessionsCount()
        {
            _logger.LogInformation("Request received to get today's physio sessions count.");
            var response = await _physioSessionStatService.GetTodayPhysioSessionsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}