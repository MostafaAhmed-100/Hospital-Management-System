using HospitalManagementSystem.DTOs.InsuranceProviderDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.InsuranceProviderService
{
    public interface IInsuranceProviderService
    {
        Task<ApiResponseDto<InsuranceProviderWithPatientsResponseDto>> GetProviderWithPatientsAsync(int id);
        Task<ApiResponseDto<PagedResultDto<InsuranceProviderResponseDto>>> GetAllProvidersAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<InsuranceProviderResponseDto>> GetProviderByIdAsync(int id);
        Task<ApiResponseDto<InsuranceProviderResponseDto>> CreateProviderAsync(CreateInsuranceProviderDto dto);
        Task<ApiResponseDto<string>> UpdateProviderAsync(UpdateInsuranceProviderDto dto);
        Task<ApiResponseDto<string>> DeleteProviderAsync(int id);
    }
}