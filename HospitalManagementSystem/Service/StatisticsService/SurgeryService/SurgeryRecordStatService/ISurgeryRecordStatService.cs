using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;

namespace HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryRecordStatService
{
    public interface ISurgeryRecordStatService
    {
        Task<ApiResponseDto<IEnumerable<SurgeryStatusDistributionDto>>> GetSurgeryStatusDistributionAsync();
        Task<ApiResponseDto<IEnumerable<TopSurgeryTypeDto>>> GetTopSurgeryTypesAsync();
    }
}