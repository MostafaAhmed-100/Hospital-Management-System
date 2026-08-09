using HospitalManagementSystem.Service.StatisticsService.LabTestService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.LabTestControllers
{
    [Route("api/statistics/lab-tests")]
    [ApiController]
    public class LabTestStatisticsController : ControllerBase
    {
        private readonly ILabTestStatService _labTestStatService;
        private readonly ILogger<LabTestStatisticsController> _logger;

        public LabTestStatisticsController(
            ILabTestStatService labTestStatService,
            ILogger<LabTestStatisticsController> logger)
        {
            _labTestStatService = labTestStatService;
            _logger = logger;
        }

        [HttpGet("status-distribution")]
        public async Task<IActionResult> GetLabTestStatusDistribution()
        {
            _logger.LogInformation("Request received to get lab test status distribution.");
            var response = await _labTestStatService.GetLabTestStatusDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("top-requested")]
        public async Task<IActionResult> GetTopRequestedLabTests()
        {
            _logger.LogInformation("Request received to get top requested lab tests.");
            var response = await _labTestStatService.GetTopRequestedLabTestsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
