using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PhysiotherapyService.TherapistService
{
    public interface ITherapistService
    {
        Task<ApiResponseDto<PagedResultDto<TherapistResponseDto>>> GetAllTherapistsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<TherapistResponseDto>> GetTherapistByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<TherapistResponseDto>>> GetTherapistsByDepartmentIdAsync(int departmentId);
        Task<ApiResponseDto<TherapistResponseDto>> CreateTherapistAsync(CreateTherapistDto dto);
        Task<ApiResponseDto<string>> UpdateTherapistAsync(UpdateTherapistDto dto);
        Task<ApiResponseDto<string>> DeleteTherapistAsync(int id);
    }
}
