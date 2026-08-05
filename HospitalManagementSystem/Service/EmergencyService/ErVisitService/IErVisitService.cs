using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.EmergencyService.ErVisitService
{
    public interface IErVisitService
    {
        Task<ApiResponseDto<PagedResultDto<ErVisitDto>>> GetAllErVisitsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<ErVisitDto>> GetErVisitByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<ErVisitDto>>> GetErQueueAsync();
        Task<ApiResponseDto<ErVisitDto>> CreateErVisitAsync(CreateErVisitDto dto);
        Task<ApiResponseDto<string>> UpdateErVisitAsync(UpdateErVisitDto dto);
        Task<ApiResponseDto<string>> DeleteErVisitAsync(int id);
    }
}
