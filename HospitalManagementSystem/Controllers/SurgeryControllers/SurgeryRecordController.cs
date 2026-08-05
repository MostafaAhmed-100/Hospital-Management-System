using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;
using HospitalManagementSystem.Service.SurgeryService.SurgeryRecordService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class SurgeryRecordController : ControllerBase
    {
        private readonly ISurgeryRecordService _surgeryRecordService;

        public SurgeryRecordController(ISurgeryRecordService surgeryRecordService)
        {
            _surgeryRecordService = surgeryRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _surgeryRecordService.GetAllSurgeryRecordsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _surgeryRecordService.GetSurgeryRecordByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateSurgeryRecordDto dto)
        {
            var result = await _surgeryRecordService.CreateSurgeryRecordAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateSurgeryRecordDto dto)
        {
            var result = await _surgeryRecordService.UpdateSurgeryRecordAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _surgeryRecordService.DeleteSurgeryRecordAsync(id);
            return Ok(result);
        }
    }
}