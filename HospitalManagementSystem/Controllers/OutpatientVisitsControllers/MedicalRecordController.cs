using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs;
using HospitalManagementSystem.Service.OutpatientVisitsService.MedicalRecordService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers.OutpatientVisitsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _medicalRecordService.GetAllMedicalRecordsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicalRecordService.GetMedicalRecordByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            var result = await _medicalRecordService.GetRecordsByPatientIdAsync(patientId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateMedicalRecordDto dto)
        {
            var result = await _medicalRecordService.CreateMedicalRecordAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateMedicalRecordDto dto)
        {
            var result = await _medicalRecordService.UpdateMedicalRecordAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicalRecordService.DeleteMedicalRecordAsync(id);
            return Ok(result);
        }
    }
}