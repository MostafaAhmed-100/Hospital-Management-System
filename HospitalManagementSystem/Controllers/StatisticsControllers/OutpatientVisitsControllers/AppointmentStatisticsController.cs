using HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.AppointmentStatService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.OutpatientVisitsControllers
{
    [Route("api/statistics/appointments")]
    [ApiController]
    public class AppointmentStatisticsController : ControllerBase
    {
        private readonly IAppointmentStatService _appointmentStatService;
        private readonly ILogger<AppointmentStatisticsController> _logger;

        public AppointmentStatisticsController(
            IAppointmentStatService appointmentStatService,
            ILogger<AppointmentStatisticsController> logger)
        {
            _appointmentStatService = appointmentStatService;
            _logger = logger;
        }
        [HttpGet("status-distribution")]
        public async Task<IActionResult> GetAppointmentsStatusDistribution()
        {
            _logger.LogInformation("Request received to get appointments distribution by status.");
            var response = await _appointmentStatService.GetAppointmentsDistributionByStatusAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("today-count")]
        public async Task<IActionResult> GetTodayAppointmentsCount()
        {
            _logger.LogInformation("Request received to get today's appointments count.");
            var response = await _appointmentStatService.GetTodayAppointmentsCountAsync();

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
