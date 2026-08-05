using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;
using HospitalManagementSystem.Service.NursingStaffService.NurseAssignmentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class NurseAssignmentController : ControllerBase
    {
        private readonly INurseAssignmentService _nurseAssignmentService;

        public NurseAssignmentController(INurseAssignmentService nurseAssignmentService)
        {
            _nurseAssignmentService = nurseAssignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _nurseAssignmentService.GetAllAssignmentsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _nurseAssignmentService.GetAssignmentByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("nurse/{nurseId}")]
        public async Task<IActionResult> GetByNurseId(int nurseId)
        {
            var result = await _nurseAssignmentService.GetAssignmentsByNurseIdAsync(nurseId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateNurseAssignmentDto dto)
        {
            var result = await _nurseAssignmentService.CreateAssignmentAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateNurseAssignmentDto dto)
        {
            var result = await _nurseAssignmentService.UpdateAssignmentAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _nurseAssignmentService.DeleteAssignmentAsync(id);
            return Ok(result);
        }
    }
}