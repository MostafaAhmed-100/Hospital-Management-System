using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.OutpatientVisitsService.MedicalRecordService
{
    public interface IMedicalRecordService
    {
        Task<ApiResponseDto<PagedResultDto<MedicalRecordResponseDto>>> GetAllMedicalRecordsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<MedicalRecordResponseDto>> GetMedicalRecordByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<MedicalRecordResponseDto>>> GetRecordsByPatientIdAsync(int patientId);
        Task<ApiResponseDto<MedicalRecordResponseDto>> CreateMedicalRecordAsync(CreateMedicalRecordDto dto);
        Task<ApiResponseDto<string>> UpdateMedicalRecordAsync(UpdateMedicalRecordDto dto);
        Task<ApiResponseDto<string>> DeleteMedicalRecordAsync(int id);
    }
}