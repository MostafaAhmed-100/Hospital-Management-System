using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.AdmissionStatService
{
    public interface IAdmissionStatService
    {
        Task<ApiResponseDto<int>> GetActiveAdmissionsCountAsync();
        Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTopAdmittingDoctorsAsync();
    }
}
