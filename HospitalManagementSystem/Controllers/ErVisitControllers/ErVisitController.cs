using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;
using HospitalManagementSystem.Service.EmergencyService.ErVisitService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class ErVisitController : ControllerBase
    {
        private readonly IErVisitService _erVisitService;

        public ErVisitController(IErVisitService erVisitService)
        {
            _erVisitService = erVisitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _erVisitService.GetAllErVisitsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _erVisitService.GetErVisitByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("queue")]
        public async Task<IActionResult> GetQueue()
        {
            var result = await _erVisitService.GetErQueueAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateErVisitDto dto)
        {
            var result = await _erVisitService.CreateErVisitAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateErVisitDto dto)
        {
            var result = await _erVisitService.UpdateErVisitAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _erVisitService.DeleteErVisitAsync(id);
            return Ok(result);
        }
    }
}