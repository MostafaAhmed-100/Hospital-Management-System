using HospitalManagementSystem.DTOs.SaleItemDTOs;
using HospitalManagementSystem.Service.SaleItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class SaleItemController : ControllerBase
    {
        private readonly ISaleItemService _itemService;

        public SaleItemController(ISaleItemService itemService)
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

        [HttpGet("sale/{saleId}")]
        public async Task<IActionResult> GetBySaleId(int saleId)
        {
            var result = await _itemService.GetItemsBySaleIdAsync(saleId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateSaleItemDto dto)
        {
            var result = await _itemService.CreateItemAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateSaleItemDto dto)
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