using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;
using HospitalManagementSystem.Service.SurgeryService.OperatingRoomService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class OperatingRoomController : ControllerBase
    {
        private readonly IOperatingRoomService _operatingRoomService;

        public OperatingRoomController(IOperatingRoomService operatingRoomService)
        {
            _operatingRoomService = operatingRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _operatingRoomService.GetAllOperatingRoomsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _operatingRoomService.GetOperatingRoomByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var result = await _operatingRoomService.GetAvailableOperatingRoomsAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateOperatingRoomDto dto)
        {
            var result = await _operatingRoomService.CreateOperatingRoomAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateOperatingRoomDto dto)
        {
            var result = await _operatingRoomService.UpdateOperatingRoomAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operatingRoomService.DeleteOperatingRoomAsync(id);
            return Ok(result);
        }
    }
}