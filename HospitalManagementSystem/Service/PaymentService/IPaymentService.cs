using HospitalManagementSystem.DTOs.PaymentDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<ApiResponseDto<PagedResultDto<PaymentResponseDto>>> GetAllPaymentsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PaymentResponseDto>> GetPaymentByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<PaymentResponseDto>>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ApiResponseDto<PaymentResponseDto>> CreatePaymentAsync(CreatePaymentDto dto);
        Task<ApiResponseDto<string>> UpdatePaymentAsync(UpdatePaymentDto dto);
        Task<ApiResponseDto<string>> DeletePaymentAsync(int id);
    }
}