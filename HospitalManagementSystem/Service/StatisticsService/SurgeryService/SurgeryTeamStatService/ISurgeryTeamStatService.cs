using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;

namespace HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryTeamStatService
{
    public interface ISurgeryTeamStatService
    {
        Task<ApiResponseDto<IEnumerable<SurgeryRoleDistributionDto>>> GetSurgeryRoleDistributionAsync();
        Task<ApiResponseDto<IEnumerable<TopActiveSurgeryStaffDto>>> GetTopActiveSurgeryStaffAsync();
    }
}
