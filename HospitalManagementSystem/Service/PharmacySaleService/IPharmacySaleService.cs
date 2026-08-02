using HospitalManagementSystem.DTOs.PharmacySaleDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PharmacySaleService
{
    public interface IPharmacySaleService
    {
        Task<ApiResponseDto<PagedResultDto<PharmacySaleResponseDto>>> GetAllSalesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PharmacySaleResponseDto>> GetSaleByIdAsync(int id);
        Task<ApiResponseDto<PharmacySaleWithItemsResponseDto>> GetSaleWithItemsAsync(int id);
        Task<ApiResponseDto<PharmacySaleResponseDto>> CreateSaleAsync(CreatePharmacySaleDto dto);
        Task<ApiResponseDto<string>> UpdateSaleAsync(UpdatePharmacySaleDto dto);
        Task<ApiResponseDto<string>> DeleteSaleAsync(int id);
    }
}