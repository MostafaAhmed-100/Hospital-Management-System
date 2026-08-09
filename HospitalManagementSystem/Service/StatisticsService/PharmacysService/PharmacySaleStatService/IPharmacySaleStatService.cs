using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacySaleStatService
{
    public interface IPharmacySaleStatService
    {
        Task<ApiResponseDto<IEnumerable<PharmacyRevenueDto>>> GetTotalRevenueByPharmacyAsync();
        Task<ApiResponseDto<IEnumerable<SalePrescriptionDistributionDto>>> GetSalesDistributionByPrescriptionAsync();
    }
}
