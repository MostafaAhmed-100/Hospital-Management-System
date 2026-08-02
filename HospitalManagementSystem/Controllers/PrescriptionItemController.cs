using HospitalManagementSystem.DTOs.PrescriptionItemDTOs;
using HospitalManagementSystem.Service.PrescriptionItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class PrescriptionItemController : ControllerBase
    {
        private readonly IPrescriptionItemService _itemService;

        public PrescriptionItemController(IPrescriptionItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _itemService.GetAllItemsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _itemService.GetItemByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("prescription/{prescriptionId}")]
        public async Task<IActionResult> GetByPrescriptionId(int prescriptionId)
        {
            var result = await _itemService.GetItemsByPrescriptionIdAsync(prescriptionId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionItemDto dto)
        {
            var result = await _itemService.CreateItemAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdatePrescriptionItemDto dto)
        {
            var result = await _itemService.UpdateItemAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            return Ok(result);
        }
    }
}