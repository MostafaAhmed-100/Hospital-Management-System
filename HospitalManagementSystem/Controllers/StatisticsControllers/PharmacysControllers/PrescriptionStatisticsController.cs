using HospitalManagementSystem.Service.StatisticsService.PharmacysService.PrescriptionStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/prescriptions")]
    [ApiController]
    public class PrescriptionStatisticsController : ControllerBase
    {
        private readonly IPrescriptionStatService _prescriptionStatService;
        private readonly ILogger<PrescriptionStatisticsController> _logger;

        public PrescriptionStatisticsController(
            IPrescriptionStatService prescriptionStatService,
            ILogger<PrescriptionStatisticsController> logger)
        {
            _prescriptionStatService = prescriptionStatService;
            _logger = logger;
        }
        [HttpGet("status-distribution")]
        public async Task<IActionResult> GetPrescriptionStatusDistribution()
        {
            _logger.LogInformation("Request received to get prescription status distribution.");
            var response = await _prescriptionStatService.GetPrescriptionStatusDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("top-doctors")]
        public async Task<IActionResult> GetTopPrescribingDoctors()
        {
            _logger.LogInformation("Request received to get top prescribing doctors.");
            var response = await _prescriptionStatService.GetTopPrescribingDoctorsAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}