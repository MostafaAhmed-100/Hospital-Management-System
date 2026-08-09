using HospitalManagementSystem.Service.StatisticsService.PharmacysService.MedicineStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/medicines")]
    [ApiController]
    public class MedicineStatisticsController : ControllerBase
    {
        private readonly IMedicineStatService _medicineStatService;
        private readonly ILogger<MedicineStatisticsController> _logger;

        public MedicineStatisticsController(
            IMedicineStatService medicineStatService,
            ILogger<MedicineStatisticsController> logger)
        {
            _medicineStatService = medicineStatService;
            _logger = logger;
        }

        [HttpGet("top-selling")]
        public async Task<IActionResult> GetTopSellingMedicines()
        {
            _logger.LogInformation("Request received to get top selling medicines.");
            var response = await _medicineStatService.GetTopSellingMedicinesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("prescription-distribution")]
        public async Task<IActionResult> GetMedicinePrescriptionDistribution()
        {
            _logger.LogInformation("Request received to get medicine prescription distribution.");
            var response = await _medicineStatService.GetMedicinePrescriptionDistributionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
