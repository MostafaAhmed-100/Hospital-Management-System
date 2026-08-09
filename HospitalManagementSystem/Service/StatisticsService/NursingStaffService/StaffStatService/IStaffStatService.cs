using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.StaffStatService
{
    public interface IStaffStatService
    {
        Task<ApiResponseDto<IEnumerable<StaffRoleDistributionDto>>> GetStaffDistributionByRoleAsync();
        Task<ApiResponseDto<IEnumerable<ClinicStaffCountDto>>> GetTopClinicsByStaffCountAsync();
    }
}
