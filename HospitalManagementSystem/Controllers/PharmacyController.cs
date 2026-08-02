using HospitalManagementSystem.DTOs.PharmacyDTOs;
using HospitalManagementSystem.Service.PharmacyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _pharmacyService.GetAllPharmaciesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pharmacyService.GetPharmacyByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/inventory")]
        public async Task<IActionResult> GetWithInventory(int id)
        {
            var result = await _pharmacyService.GetPharmacyWithInventoryAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreatePharmacyDto dto)
        {
            var result = await _pharmacyService.CreatePharmacyAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdatePharmacyDto dto)
        {
            var result = await _pharmacyService.UpdatePharmacyAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pharmacyService.DeletePharmacyAsync(id);
            return Ok(result);
        }
    }
}