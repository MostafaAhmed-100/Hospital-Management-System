using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.EmergencyService
{
    public interface IErVisitStatService
    {
        Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTopDoctorsInErAsync();
        Task<ApiResponseDto<int>> GetActiveErVisitsCountAsync();
        Task<ApiResponseDto<IEnumerable<TriageDistributionDto>>> GetErVisitsDistributionAsync();

    }
}
