using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs;

namespace HospitalManagementSystem.Service.SurgeryService.SurgeryTeamService
{
    public interface ISurgeryTeamService
    {
        Task<ApiResponseDto<PagedResultDto<SurgeryTeamResponseDto>>> GetAllSurgeryTeamsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<SurgeryTeamResponseDto>> GetSurgeryTeamByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<SurgeryTeamResponseDto>>> GetTeamBySurgeryIdAsync(int surgeryId);
        Task<ApiResponseDto<SurgeryTeamResponseDto>> CreateSurgeryTeamAsync(CreateSurgeryTeamDto dto);
        Task<ApiResponseDto<string>> UpdateSurgeryTeamAsync(UpdateSurgeryTeamDto dto);
        Task<ApiResponseDto<string>> DeleteSurgeryTeamAsync(int id);
    }
}
