using HospitalManagementSystem.DTOs.PharmacyInventoryDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PharmacyInventoryService
{
    public interface IPharmacyInventoryService
    {
        Task<ApiResponseDto<PagedResultDto<PharmacyInventoryResponseDto>>> GetAllInventoryAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PharmacyInventoryResponseDto>> GetInventoryByIdAsync(int id);
        Task<ApiResponseDto<PharmacyInventoryResponseDto>> CheckMedicineStockAsync(int pharmacyId, int medicineId);
        Task<ApiResponseDto<PharmacyInventoryResponseDto>> CreateOrUpdateInventoryAsync(CreatePharmacyInventoryDto dto);
        Task<ApiResponseDto<string>> UpdateInventoryAsync(UpdatePharmacyInventoryDto dto);
        Task<ApiResponseDto<string>> DeleteInventoryAsync(int id);
    }
}