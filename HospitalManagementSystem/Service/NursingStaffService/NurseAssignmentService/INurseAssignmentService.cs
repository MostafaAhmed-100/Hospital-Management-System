using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;

namespace HospitalManagementSystem.Service.NursingStaffService.NurseAssignmentService
{
    public interface INurseAssignmentService
    {
        Task<ApiResponseDto<PagedResultDto<NurseAssignmentResponseDto>>> GetAllAssignmentsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<NurseAssignmentResponseDto>> GetAssignmentByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<NurseAssignmentResponseDto>>> GetAssignmentsByNurseIdAsync(int nurseId);
        Task<ApiResponseDto<NurseAssignmentResponseDto>> CreateAssignmentAsync(CreateNurseAssignmentDto dto);
        Task<ApiResponseDto<string>> UpdateAssignmentAsync(UpdateNurseAssignmentDto dto);
        Task<ApiResponseDto<string>> DeleteAssignmentAsync(int id);
    }
}