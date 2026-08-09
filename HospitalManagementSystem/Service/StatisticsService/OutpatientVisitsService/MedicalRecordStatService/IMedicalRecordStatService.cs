using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.MedicalRecordStatService
{
    public interface IMedicalRecordStatService
    {
        Task<ApiResponseDto<IEnumerable<TopDiagnosisDto>>> GetTopDiagnosesAsync();
        Task<ApiResponseDto<int>> GetTodayMedicalRecordsCountAsync();
    }
}
