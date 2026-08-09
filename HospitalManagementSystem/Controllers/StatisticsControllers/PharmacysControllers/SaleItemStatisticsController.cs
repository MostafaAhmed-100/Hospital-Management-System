using HospitalManagementSystem.Service.StatisticsService.PharmacysService.SaleItemStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/sale-items")]
    [ApiController]
    public class SaleItemStatisticsController : ControllerBase
    {
        private readonly ISaleItemStatService _saleItemStatService;
        private readonly ILogger<SaleItemStatisticsController> _logger;

        public SaleItemStatisticsController(
            ISaleItemStatService saleItemStatService,
            ILogger<SaleItemStatisticsController> logger)
        {
            _saleItemStatService = saleItemStatService;
            _logger = logger;
        }
        [HttpGet("top-revenue")]
        public async Task<IActionResult> GetTopRevenueGeneratingMedicines()
        {
            _logger.LogInformation("Request received to get top revenue-generating medicines.");
            var response = await _saleItemStatService.GetTopRevenueGeneratingMedicinesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
