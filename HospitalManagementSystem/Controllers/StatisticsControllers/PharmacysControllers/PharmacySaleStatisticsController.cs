using HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacySaleStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/pharmacy-sales")]
    [ApiController]
    public class PharmacySaleStatisticsController : ControllerBase
    {
        private readonly IPharmacySaleStatService _saleStatService;
        private readonly ILogger<PharmacySaleStatisticsController> _logger;

        public PharmacySaleStatisticsController(
            IPharmacySaleStatService saleStatService,
            ILogger<PharmacySaleStatisticsController> logger)
        {
            _saleStatService = saleStatService;
            _logger = logger;
        }
        [HttpGet("revenue-by-pharmacy")]
        public async Task<IActionResult> GetTotalRevenueByPharmacy()
        {
            _logger.LogInformation("Request received to get total revenue by pharmacy.");
            var response = await _saleStatService.GetTotalRevenueByPharmacyAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("prescription-distribution")]
        public async Task<IActionResult> GetSalesDistributionByPrescription()
        {
            _logger.LogInformation("Request received to get sales distribution by prescription.");
            var response = await _saleStatService.GetSalesDistributionByPrescriptionAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
