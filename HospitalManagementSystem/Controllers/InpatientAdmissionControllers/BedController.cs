using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;
using HospitalManagementSystem.Service.InpatientService.BedService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class BedController : ControllerBase
    {
        private readonly IBedService _bedService;

        public BedController(IBedService bedService)
        {
            _bedService = bedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _bedService.GetAllBedsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bedService.GetBedByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBeds()
        {
            var result = await _bedService.GetAvailableBedsAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateBedDto dto)
        {
            var result = await _bedService.CreateBedAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateBedDto dto)
        {
            var result = await _bedService.UpdateBedAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bedService.DeleteBedAsync(id);
            return Ok(result);
        }
    }
}