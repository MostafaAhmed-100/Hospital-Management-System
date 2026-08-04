using HospitalManagementSystem.DTOs.InvoiceDTOs;
using HospitalManagementSystem.Service.InvoiceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Standard")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _invoiceService.GetAllInvoicesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _invoiceService.GetInvoiceByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetWithPayments(int id)
        {
            var result = await _invoiceService.GetInvoiceWithPaymentsAsync(id);
            return Ok(result);
        }

        [HttpGet("patient/{patientId}/unpaid")]
        public async Task<IActionResult> GetUnpaidByPatient(int patientId)
        {
            var result = await _invoiceService.GetUnpaidInvoicesByPatientAsync(patientId);
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _invoiceService.CreateInvoiceAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceDto dto)
        {
            var result = await _invoiceService.UpdateInvoiceAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("Strict")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invoiceService.DeleteInvoiceAsync(id);
            return Ok(result);
        }
    }
}