using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;
using HospitalManagementSystem.Service.NursingStaffService.NurseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class NurseController : ControllerBase
    {
        private readonly INurseService _nurseService;

        public NurseController(INurseService nurseService)
        {
            _nurseService = nurseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _nurseService.GetAllNursesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _nurseService.GetNurseByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("shift/{shift}")]
        public async Task<IActionResult> GetByShift(ShiftType shift)
        {
            var result = await _nurseService.GetNursesByShiftAsync(shift);
            return Ok(result);
        }

        [HttpGet("ward/{ward}")]
        public async Task<IActionResult> GetByWard(string ward)
        {
            var result = await _nurseService.GetNursesByWardAsync(ward);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateNurseDto dto)
        {
            var result = await _nurseService.CreateNurseAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateNurseDto dto)
        {
            var result = await _nurseService.UpdateNurseAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _nurseService.DeleteNurseAsync(id);
            return Ok(result);
        }
    }
}