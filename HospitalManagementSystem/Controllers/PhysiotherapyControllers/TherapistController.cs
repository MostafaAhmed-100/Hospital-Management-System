using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;
using HospitalManagementSystem.Service.PhysiotherapyService.TherapistService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistService _therapistService;

        public TherapistController(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _therapistService.GetAllTherapistsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _therapistService.GetTherapistByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentId(int departmentId)
        {
            var result = await _therapistService.GetTherapistsByDepartmentIdAsync(departmentId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateTherapistDto dto)
        {
            var result = await _therapistService.CreateTherapistAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateTherapistDto dto)
        {
            var result = await _therapistService.UpdateTherapistAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _therapistService.DeleteTherapistAsync(id);
            return Ok(result);
        }
    }
}