using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;

namespace HospitalManagementSystem.Service.SurgeryService.SurgeryRecordService
{
    public interface ISurgeryRecordService
    {
        Task<ApiResponseDto<PagedResultDto<SurgeryRecordResponseDto>>> GetAllSurgeryRecordsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<SurgeryRecordResponseDto>> GetSurgeryRecordByIdAsync(int id);
        Task<ApiResponseDto<SurgeryRecordResponseDto>> CreateSurgeryRecordAsync(CreateSurgeryRecordDto dto);
        Task<ApiResponseDto<string>> UpdateSurgeryRecordAsync(UpdateSurgeryRecordDto dto);
        Task<ApiResponseDto<string>> DeleteSurgeryRecordAsync(int id);
    }
}