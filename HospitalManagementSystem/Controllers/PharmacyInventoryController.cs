using HospitalManagementSystem.DTOs.PharmacyInventoryDTOs;
using HospitalManagementSystem.Service.PharmacyInventoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PharmacyInventoryController : ControllerBase
    {
        private readonly IPharmacyInventoryService _inventoryService;

        public PharmacyInventoryController(IPharmacyInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _inventoryService.GetAllInventoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _inventoryService.GetInventoryByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("check-stock")]
        public async Task<IActionResult> CheckStock([FromQuery] int pharmacyId, [FromQuery] int medicineId)
        {
            var result = await _inventoryService.CheckMedicineStockAsync(pharmacyId, medicineId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CreatePharmacyInventoryDto dto)
        {
            var result = await _inventoryService.CreateOrUpdateInventoryAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdatePharmacyInventoryDto dto)
        {
            var result = await _inventoryService.UpdateInventoryAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _inventoryService.DeleteInventoryAsync(id);
            return Ok(result);
        }
    }
}