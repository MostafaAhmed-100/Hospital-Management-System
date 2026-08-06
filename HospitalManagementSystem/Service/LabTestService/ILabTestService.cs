using HospitalManagementSystem.DTOs.LabTestDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.LabTestService
{
    public interface ILabTestService
    {
        Task<ApiResponseDto<PagedResultDto<LabTestResponseDto>>> GetAllLabTestsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<LabTestResponseDto>> GetLabTestByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<LabTestResponseDto>>> GetTestsByRecordIdAsync(int recordId);
        Task<ApiResponseDto<IEnumerable<LabTestResponseDto>>> GetPendingTestsAsync();
        Task<ApiResponseDto<LabTestResponseDto>> CreateLabTestAsync(CreateLabTestDto dto);
        Task<ApiResponseDto<string>> UpdateLabTestResultAsync(UpdateLabTestResultDto dto);
        Task<ApiResponseDto<string>> DeleteLabTestAsync(int id);
    }
}
