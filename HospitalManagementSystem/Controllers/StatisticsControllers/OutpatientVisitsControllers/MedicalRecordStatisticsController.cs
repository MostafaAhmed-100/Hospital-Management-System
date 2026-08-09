using HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.MedicalRecordStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.OutpatientVisitsControllers
{
    [Route("api/statistics/medical-records")]
    [ApiController]
    public class MedicalRecordStatisticsController : ControllerBase
    {
        private readonly IMedicalRecordStatService _medicalRecordStatService;
        private readonly ILogger<MedicalRecordStatisticsController> _logger;

        public MedicalRecordStatisticsController(
            IMedicalRecordStatService medicalRecordStatService,
            ILogger<MedicalRecordStatisticsController> logger)
        {
            _medicalRecordStatService = medicalRecordStatService;
            _logger = logger;
        }
        [HttpGet("top-diagnoses")]
        public async Task<IActionResult> GetTopDiagnoses()
        {
            _logger.LogInformation("Request received to get top diagnoses.");
            var response = await _medicalRecordStatService.GetTopDiagnosesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        // GET: api/statistics/medical-records/today-count
        [HttpGet("today-count")]
        public async Task<IActionResult> GetTodayMedicalRecordsCount()
        {
            _logger.LogInformation("Request received to get today's medical records count.");
            var response = await _medicalRecordStatService.GetTodayMedicalRecordsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
