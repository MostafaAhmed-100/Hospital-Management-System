using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseAssignmentStatService
{
    public interface INurseAssignmentStatService
    {
        Task<ApiResponseDto<IEnumerable<AssignmentShiftDistributionDto>>> GetAssignmentsDistributionByShiftAsync();
        Task<ApiResponseDto<IEnumerable<TopAssignedNurseDto>>> GetTopAssignedNursesAsync();
    }
}
