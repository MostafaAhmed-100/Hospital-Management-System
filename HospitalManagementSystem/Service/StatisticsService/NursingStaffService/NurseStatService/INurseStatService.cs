using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseStatService
{
    public interface INurseStatService
    {
        Task<ApiResponseDto<IEnumerable<NurseShiftDistributionDto>>> GetNursesDistributionByShiftAsync();
        Task<ApiResponseDto<IEnumerable<WardSpecializationCountDto>>> GetTopWardSpecializationsAsync();
    }
}
