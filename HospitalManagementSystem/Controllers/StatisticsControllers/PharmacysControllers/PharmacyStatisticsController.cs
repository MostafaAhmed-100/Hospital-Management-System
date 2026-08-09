using HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/pharmacies")]
    [ApiController]
    public class PharmacyStatisticsController : ControllerBase
    {
        private readonly IPharmacyStatService _pharmacyStatService;
        private readonly ILogger<PharmacyStatisticsController> _logger;

        public PharmacyStatisticsController(
            IPharmacyStatService pharmacyStatService,
            ILogger<PharmacyStatisticsController> logger)
        {
            _pharmacyStatService = pharmacyStatService;
            _logger = logger;
        }
        [HttpGet("top-sales")]
        public async Task<IActionResult> GetTopPharmaciesBySalesCount()
        {
            _logger.LogInformation("Request received to get top pharmacies by sales count.");
            var response = await _pharmacyStatService.GetTopPharmaciesBySalesCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("top-inventory")]
        public async Task<IActionResult> GetTopPharmaciesByInventorySize()
        {
            _logger.LogInformation("Request received to get top pharmacies by inventory size.");
            var response = await _pharmacyStatService.GetTopPharmaciesByInventorySizeAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
