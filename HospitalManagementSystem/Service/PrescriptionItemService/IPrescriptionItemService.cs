using HospitalManagementSystem.DTOs.PrescriptionItemDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PrescriptionItemService
{
    public interface IPrescriptionItemService
    {
        Task<ApiResponseDto<PagedResultDto<PrescriptionItemResponseDto>>> GetAllItemsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PrescriptionItemResponseDto>> GetItemByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<PrescriptionItemResponseDto>>> GetItemsByPrescriptionIdAsync(int prescriptionId);
        Task<ApiResponseDto<PrescriptionItemResponseDto>> CreateItemAsync(CreatePrescriptionItemDto dto);
        Task<ApiResponseDto<string>> UpdateItemAsync(UpdatePrescriptionItemDto dto);
        Task<ApiResponseDto<string>> DeleteItemAsync(int id);
    }
}