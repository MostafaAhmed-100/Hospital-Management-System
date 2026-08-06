using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.Service.DoctorService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetAll-Doctor")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _doctorService.GetAllDoctorsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById-Doctor-{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Creat-Doctor")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var result = await _doctorService.CreateDoctorAsync(dto);
            return Ok(result);
        }

        [HttpPut("Update-Doctor")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto)
        {
            var result = await _doctorService.UpdateDoctorAsync(dto);
            return Ok(result);
        }

        [HttpDelete("Delete-Doctor-{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            return Ok(result);
        }
    }
}