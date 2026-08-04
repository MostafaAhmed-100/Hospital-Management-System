using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs;
using HospitalManagementSystem.Service.PharmacysService.PharmacySaleService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers.PharmacysControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PharmacySaleController : ControllerBase
    {
        private readonly IPharmacySaleService _saleService;

        public PharmacySaleController(IPharmacySaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _saleService.GetAllSalesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _saleService.GetSaleByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetWithItems(int id)
        {
            var result = await _saleService.GetSaleWithItemsAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreatePharmacySaleDto dto)
        {
            var result = await _saleService.CreateSaleAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdatePharmacySaleDto dto)
        {
            var result = await _saleService.UpdateSaleAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _saleService.DeleteSaleAsync(id);
            return Ok(result);
        }
    }
}