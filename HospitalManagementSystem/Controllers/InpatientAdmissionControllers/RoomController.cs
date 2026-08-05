using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;
using HospitalManagementSystem.Service.InpatientService.RoomService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _roomService.GetAllRoomsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roomService.GetRoomByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentId(int departmentId)
        {
            var result = await _roomService.GetRoomsByDepartmentIdAsync(departmentId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var result = await _roomService.CreateRoomAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateRoomDto dto)
        {
            var result = await _roomService.UpdateRoomAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            return Ok(result);
        }
    }
}