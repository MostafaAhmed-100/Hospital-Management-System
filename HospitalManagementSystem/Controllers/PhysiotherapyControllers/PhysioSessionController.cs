using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;
using HospitalManagementSystem.Service.PhysiotherapyService.PhysioSessionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PhysioSessionController : ControllerBase
    {
        private readonly IPhysioSessionService _physioSessionService;

        public PhysioSessionController(IPhysioSessionService physioSessionService)
        {
            _physioSessionService = physioSessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _physioSessionService.GetAllSessionsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _physioSessionService.GetSessionByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            var result = await _physioSessionService.GetSessionsByPatientIdAsync(patientId);
            return Ok(result);
        }

        [HttpGet("therapist/{therapistId}")]
        public async Task<IActionResult> GetByTherapistId(int therapistId)
        {
            var result = await _physioSessionService.GetSessionsByTherapistIdAsync(therapistId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreatePhysioSessionDto dto)
        {
            var result = await _physioSessionService.CreateSessionAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdatePhysioSessionDto dto)
        {
            var result = await _physioSessionService.UpdateSessionAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _physioSessionService.DeleteSessionAsync(id);
            return Ok(result);
        }
    }
}