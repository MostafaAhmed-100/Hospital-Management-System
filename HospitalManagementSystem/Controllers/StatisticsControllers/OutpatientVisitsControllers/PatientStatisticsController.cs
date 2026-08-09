using HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.PatientStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.OutpatientVisitsControllers
{
    [Route("api/statistics/patients")]
    [ApiController]
    public class PatientStatisticsController : ControllerBase
    {
        private readonly IPatientStatService _patientStatService;
        private readonly ILogger<PatientStatisticsController> _logger;

        public PatientStatisticsController(
            IPatientStatService patientStatService,
            ILogger<PatientStatisticsController> logger)
        {
            _patientStatService = patientStatService;
            _logger = logger;
        }
        [HttpGet("insurance-distribution")]
        public async Task<IActionResult> GetPatientInsuranceDistribution()
        {
            _logger.LogInformation("Request received to get patient insurance distribution.");
            var response = await _patientStatService.GetPatientInsuranceDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("top-frequent")]
        public async Task<IActionResult> GetTopFrequentPatients()
        {
            _logger.LogInformation("Request received to get top frequent patients.");
            var response = await _patientStatService.GetTopFrequentPatientsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
