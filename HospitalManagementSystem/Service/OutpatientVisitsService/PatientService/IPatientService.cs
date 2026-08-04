using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.PatientDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.OutpatientVisitsService.PatientService
{
    public interface IPatientService
    {
        Task<ApiResponseDto<PagedResultDto<PatientResponseDto>>> GetAllPatientsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<PatientResponseDto>> GetPatientByIdAsync(int id);
        Task<ApiResponseDto<PatientWithMedicalHistoryResponseDto>> GetPatientWithMedicalHistoryAsync(int id);
        Task<ApiResponseDto<PatientResponseDto>> CreatePatientAsync(CreatePatientDto dto);
        Task<ApiResponseDto<string>> UpdatePatientAsync(UpdatePatientDto dto);
        Task<ApiResponseDto<string>> DeletePatientAsync(int id);
    }
}