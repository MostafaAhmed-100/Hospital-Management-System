using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyStatService
{
    public interface IPharmacyStatService
    {
        Task<ApiResponseDto<IEnumerable<TopPharmacyByInventoryDto>>> GetTopPharmaciesByInventorySizeAsync();
        Task<ApiResponseDto<IEnumerable<TopPharmacyBySalesDto>>> GetTopPharmaciesBySalesCountAsync();
    }
}
