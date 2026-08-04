using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PharmacysService.PrescriptionService
{
    public interface IPrescriptionService
    {
        Task<ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>> GetAllPrescriptionsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PrescriptionResponseDto>> GetPrescriptionByIdAsync(int id);
        Task<ApiResponseDto<PrescriptionWithItemsResponseDto>> GetPrescriptionWithItemsAsync(int id);
        Task<ApiResponseDto<PrescriptionResponseDto>> CreatePrescriptionAsync(CreatePrescriptionDto dto);
        Task<ApiResponseDto<string>> UpdatePrescriptionStatusAsync(UpdatePrescriptionDto dto);
        Task<ApiResponseDto<string>> DeletePrescriptionAsync(int id);
    }
}