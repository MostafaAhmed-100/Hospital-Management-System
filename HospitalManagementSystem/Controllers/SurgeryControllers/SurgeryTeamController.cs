using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs;
using HospitalManagementSystem.Service.SurgeryService.SurgeryTeamService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class SurgeryTeamController : ControllerBase
    {
        private readonly ISurgeryTeamService _surgeryTeamService;

        public SurgeryTeamController(ISurgeryTeamService surgeryTeamService)
        {
            _surgeryTeamService = surgeryTeamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _surgeryTeamService.GetAllSurgeryTeamsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _surgeryTeamService.GetSurgeryTeamByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("surgery/{surgeryId}")]
        public async Task<IActionResult> GetBySurgeryId(int surgeryId)
        {
            var result = await _surgeryTeamService.GetTeamBySurgeryIdAsync(surgeryId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateSurgeryTeamDto dto)
        {
            var result = await _surgeryTeamService.CreateSurgeryTeamAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateSurgeryTeamDto dto)
        {
            var result = await _surgeryTeamService.UpdateSurgeryTeamAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _surgeryTeamService.DeleteSurgeryTeamAsync(id);
            return Ok(result);
        }
    }
}