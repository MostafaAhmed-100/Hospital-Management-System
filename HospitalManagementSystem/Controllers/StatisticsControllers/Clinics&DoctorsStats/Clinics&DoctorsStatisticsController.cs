using HospitalManagementSystem.Service.StatisticsService.Clinics_DoctorsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers.StatisticsControllers.Clinics_DoctorsStats
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clinics_DoctorsStatisticsController : ControllerBase
    {
        private readonly IClinicsAndDoctorsStat _clinicsDoctorsStat;
        private readonly ILogger<Clinics_DoctorsStatisticsController> _logger;

        public Clinics_DoctorsStatisticsController(
            IClinicsAndDoctorsStat clinicsDoctorsStat,
            ILogger<Clinics_DoctorsStatisticsController> logger)
        {
            _clinicsDoctorsStat = clinicsDoctorsStat;
            _logger = logger;
        }

        [HttpGet("top-Clinics-in-hospital")]
        public async Task<IActionResult> GetTopClinicsInHospital()
        {
            _logger.LogInformation("Request received to get the most clinics with appointments in the hospital.");

            var response = await _clinicsDoctorsStat.GetTheMostClinicsWithAppointmentInHospital();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("top-Clinics-in-department/{departmentId}")]
        public async Task<IActionResult> GetTopClinicsInDepartment([FromRoute] int departmentId)
        {
            _logger.LogInformation("Request received to get top clinics for department {DepartmentId}.", departmentId);

            if (departmentId <= 0)
            {
                return BadRequest(new { Message = "Invalid Department ID" });
            }

            var response = await _clinicsDoctorsStat.GetTheMostClinicsWithAppointmentInDepartment(departmentId);

            if (!response.IsSuccess)
            {
                if (response.StatusCode == 404)
                {
                    return NotFound(response);
                }
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("top-Doctors-in-hospital")]
        public async Task<IActionResult> GetTopDoctorsInHospital()
        {
            _logger.LogInformation("Request received to get the most doctors with appointments in the hospital.");

            var response = await _clinicsDoctorsStat.GetTheMostDoctorsWithAppointmentsInHospital();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("top-Doctors-in-department/{departmentId}")]
        public async Task<IActionResult> GetTopDoctorsInDepartment([FromRoute] int departmentId)
        {
            _logger.LogInformation("Request received to get top doctors for department {DepartmentId}.", departmentId);

            if (departmentId <= 0)
            {
                return BadRequest(new { Message = "Invalid Department ID" });
            }

            var response = await _clinicsDoctorsStat.GetTheMostDoctorsWithAppointmentsInDepartment(departmentId);

            if (!response.IsSuccess)
            {
                if (response.StatusCode == 404)
                {
                    return NotFound(response);
                }
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}