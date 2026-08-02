using HospitalManagementSystem.DTOs.PrescriptionDTOs;
using HospitalManagementSystem.Service.PrescriptionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _prescriptionService.GetAllPrescriptionsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _prescriptionService.GetPrescriptionByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetWithItems(int id)
        {
            var result = await _prescriptionService.GetPrescriptionWithItemsAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
        {
            var result = await _prescriptionService.CreatePrescriptionAsync(dto);
            return Ok(result);
        }

        [HttpPut("status")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdatePrescriptionDto dto)
        {
            var result = await _prescriptionService.UpdatePrescriptionStatusAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _prescriptionService.DeletePrescriptionAsync(id);
            return Ok(result);
        }
    }
}