using HospitalManagementSystem.DTOs.SaleItemDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.SaleItemService
{
    public interface ISaleItemService
    {
        Task<ApiResponseDto<PagedResultDto<SaleItemResponseDto>>> GetAllItemsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<SaleItemResponseDto>> GetItemByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<SaleItemResponseDto>>> GetItemsBySaleIdAsync(int saleId);
        Task<ApiResponseDto<SaleItemResponseDto>> CreateItemAsync(CreateSaleItemDto dto);
        Task<ApiResponseDto<string>> UpdateItemAsync(UpdateSaleItemDto dto);
        Task<ApiResponseDto<string>> DeleteItemAsync(int id);
    }
}