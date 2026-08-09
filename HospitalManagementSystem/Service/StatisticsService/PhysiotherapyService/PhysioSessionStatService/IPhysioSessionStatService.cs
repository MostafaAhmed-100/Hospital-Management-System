using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.PhysioSessionStatService
{
    public interface IPhysioSessionStatService
    {
        Task<ApiResponseDto<IEnumerable<TopTherapyTypeDto>>> GetTopTherapyTypesAsync();
        Task<ApiResponseDto<int>> GetTodayPhysioSessionsCountAsync();
    }
}