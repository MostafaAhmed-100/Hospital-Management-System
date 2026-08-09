using HospitalManagementSystem.Service.StatisticsService.InpatientService.RoomStatService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.InpatientControllers
{
    [Route("api/statistics/rooms")]
    [ApiController]
    public class RoomStatisticsController : ControllerBase
    {
        private readonly IRoomStatService _roomStatService;
        private readonly ILogger<RoomStatisticsController> _logger;

        public RoomStatisticsController(
            IRoomStatService roomStatService,
            ILogger<RoomStatisticsController> logger)
        {
            _roomStatService = roomStatService;
            _logger = logger;
        }

        [HttpGet("type-distribution")]
        public async Task<IActionResult> GetRoomsTypeDistribution()
        {
            _logger.LogInformation("Request received to get rooms type distribution.");
            var response = await _roomStatService.GetRoomsDistributionByTypeAsync();

            if (!response.IsSuccess) 
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("top-departments")]
        public async Task<IActionResult> GetTopDepartmentsByRoomCount()
        {
            _logger.LogInformation("Request received to get top departments by room count.");
            var response = await _roomStatService.GetTopDepartmentsByRoomCountAsync();

            if (!response.IsSuccess) 
                return BadRequest(response);
            return Ok(response);
        }
    }
}
