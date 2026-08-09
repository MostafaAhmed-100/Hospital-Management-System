using HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyInventoryStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.PharmacysControllers
{
    [Route("api/statistics/pharmacy-inventories")]
    [ApiController]
    public class PharmacyInventoryStatisticsController : ControllerBase
    {
        private readonly IPharmacyInventoryStatService _inventoryStatService;
        private readonly ILogger<PharmacyInventoryStatisticsController> _logger;

        public PharmacyInventoryStatisticsController(
            IPharmacyInventoryStatService inventoryStatService,
            ILogger<PharmacyInventoryStatisticsController> logger)
        {
            _inventoryStatService = inventoryStatService;
            _logger = logger;
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockMedicines()
        {
            _logger.LogInformation("Request received to get low stock medicines.");
            var response = await _inventoryStatService.GetLowStockMedicinesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("expiring-soon")]
        public async Task<IActionResult> GetExpiringSoonMedicines()
        {
            _logger.LogInformation("Request received to get expiring soon medicines.");
            var response = await _inventoryStatService.GetExpiringSoonMedicinesAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
