using HospitalManagementSystem.DTOs.LabTestDTOs;
using HospitalManagementSystem.Service.LabTestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestService _labTestService;

        public LabTestController(ILabTestService labTestService)
        {
            _labTestService = labTestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _labTestService.GetAllLabTestsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _labTestService.GetLabTestByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetByRecordId(int recordId)
        {
            var result = await _labTestService.GetTestsByRecordIdAsync(recordId);
            return Ok(result);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _labTestService.GetPendingTestsAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateLabTestDto dto)
        {
            var result = await _labTestService.CreateLabTestAsync(dto);
            return Ok(result);
        }

        [HttpPut("result")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> UpdateResult([FromBody] UpdateLabTestResultDto dto)
        {
            var result = await _labTestService.UpdateLabTestResultAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _labTestService.DeleteLabTestAsync(id);
            return Ok(result);
        }
    }
}