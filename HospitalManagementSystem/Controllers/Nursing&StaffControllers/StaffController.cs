using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;
using HospitalManagementSystem.Service.NursingStaffService.StaffService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _staffService.GetAllStaffAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _staffService.GetStaffByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("clinic/{clinicId}")]
        public async Task<IActionResult> GetByClinicId(int clinicId)
        {
            var result = await _staffService.GetStaffByClinicIdAsync(clinicId);
            return Ok(result);
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetByRole(StaffRole role)
        {
            var result = await _staffService.GetStaffByRoleAsync(role);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateStaffDto dto)
        {
            var result = await _staffService.CreateStaffAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateStaffDto dto)
        {
            var result = await _staffService.UpdateStaffAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _staffService.DeleteStaffAsync(id);
            return Ok(result);
        }
    }
}