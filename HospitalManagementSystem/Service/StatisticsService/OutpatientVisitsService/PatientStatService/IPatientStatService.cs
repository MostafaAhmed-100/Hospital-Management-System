using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.PatientDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.PatientStatService
{
    public interface IPatientStatService
    {
        Task<ApiResponseDto<IEnumerable<TopPatientDto>>> GetTopFrequentPatientsAsync();
        Task<ApiResponseDto<IEnumerable<PatientInsuranceDistributionDto>>> GetPatientInsuranceDistributionAsync();
    }
}
