using HospitalManagementSystem.DTOs.InvoiceDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.InvoiceService
{
    public interface IInvoiceService
    {
        Task<ApiResponseDto<PagedResultDto<InvoiceResponseDto>>> GetAllInvoicesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<InvoiceResponseDto>> GetInvoiceByIdAsync(int id);
        Task<ApiResponseDto<InvoiceWithPaymentsResponseDto>> GetInvoiceWithPaymentsAsync(int id);
        Task<ApiResponseDto<IEnumerable<InvoiceResponseDto>>> GetUnpaidInvoicesByPatientAsync(int patientId);
        Task<ApiResponseDto<InvoiceResponseDto>> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<ApiResponseDto<string>> UpdateInvoiceAsync(UpdateInvoiceDto dto);
        Task<ApiResponseDto<string>> DeleteInvoiceAsync(int id);
    }
}