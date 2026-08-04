using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PharmacysService.PharmacyService
{
    public interface IPharmacyService
    {
        Task<ApiResponseDto<PagedResultDto<PharmacyResponseDto>>> GetAllPharmaciesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PharmacyResponseDto>> GetPharmacyByIdAsync(int id);
        Task<ApiResponseDto<PharmacyWithInventoryResponseDto>> GetPharmacyWithInventoryAsync(int id);
        Task<ApiResponseDto<PharmacyResponseDto>> CreatePharmacyAsync(CreatePharmacyDto dto);
        Task<ApiResponseDto<string>> UpdatePharmacyAsync(UpdatePharmacyDto dto);
        Task<ApiResponseDto<string>> DeletePharmacyAsync(int id);
    }
}