using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.BedService
{
    public interface IBedStatService
    {
        Task<ApiResponseDto<int>> GetAvailableBedsCountAsync();
        Task<ApiResponseDto<IEnumerable<BedStatusDistributionDto>>> GetBedsDistributionAsync();
    }
}
