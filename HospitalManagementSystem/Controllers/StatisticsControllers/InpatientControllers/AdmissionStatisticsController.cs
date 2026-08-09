using HospitalManagementSystem.Service.StatisticsService.InpatientService.AdmissionStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.InpatientControllers
{
    [Route("api/statistics/inpatient")]
    [ApiController]
    public class AdmissionStatisticsController : ControllerBase
    {
        private readonly IAdmissionStatService _admissionStatService;
        private readonly ILogger<AdmissionStatisticsController> _logger;

        public AdmissionStatisticsController(
            IAdmissionStatService admissionStatService,
            ILogger<AdmissionStatisticsController> logger)
        {
            _admissionStatService = admissionStatService;
            _logger = logger;
        }

        [HttpGet("active-count")]
        public async Task<IActionResult> GetActiveAdmissionsCount()
        {
            _logger.LogInformation("Request received to get active admissions count.");
            var response = await _admissionStatService.GetActiveAdmissionsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("top-doctors")]
        public async Task<IActionResult> GetTopAdmittingDoctors()
        {
            _logger.LogInformation("Request received to get top admitting doctors.");
            var response = await _admissionStatService.GetTopAdmittingDoctorsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
