using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;
using HospitalManagementSystem.Service.InpatientService.AdmissionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _admissionService;

        public AdmissionController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _admissionService.GetAllAdmissionsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _admissionService.GetAdmissionByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAdmissions()
        {
            var result = await _admissionService.GetActiveAdmissionsAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateAdmissionDto dto)
        {
            var result = await _admissionService.CreateAdmissionAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateAdmissionDto dto)
        {
            var result = await _admissionService.UpdateAdmissionAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}/discharge")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> DischargePatient(int id)
        {
            var result = await _admissionService.DischargePatientAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _admissionService.DeleteAdmissionAsync(id);
            return Ok(result);
        }
    }
}