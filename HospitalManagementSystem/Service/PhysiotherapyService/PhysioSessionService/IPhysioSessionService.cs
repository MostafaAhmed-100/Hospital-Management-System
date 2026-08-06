using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.PhysiotherapyService.PhysioSessionService
{
    public interface IPhysioSessionService
    {
        Task<ApiResponseDto<PagedResultDto<PhysioSessionResponseDto>>> GetAllSessionsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PhysioSessionResponseDto>> GetSessionByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>> GetSessionsByPatientIdAsync(int patientId);
        Task<ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>> GetSessionsByTherapistIdAsync(int therapistId);
        Task<ApiResponseDto<PhysioSessionResponseDto>> CreateSessionAsync(CreatePhysioSessionDto dto);
        Task<ApiResponseDto<string>> UpdateSessionAsync(UpdatePhysioSessionDto dto);
        Task<ApiResponseDto<string>> DeleteSessionAsync(int id);
    }
}
