using HospitalManagementSystem.DTOs.InsuranceProviderDTOs;
using HospitalManagementSystem.Service.InsuranceProviderService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class InsuranceProviderController : ControllerBase
    {
        private readonly IInsuranceProviderService _providerService;

        public InsuranceProviderController(IInsuranceProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _providerService.GetAllProvidersAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _providerService.GetProviderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/patients")]
        public async Task<IActionResult> GetWithPatients(int id)
        {
            var result = await _providerService.GetProviderWithPatientsAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateInsuranceProviderDto dto)
        {
            var result = await _providerService.CreateProviderAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateInsuranceProviderDto dto)
        {
            var result = await _providerService.UpdateProviderAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _providerService.DeleteProviderAsync(id);
            return Ok(result);
        }
    }
}