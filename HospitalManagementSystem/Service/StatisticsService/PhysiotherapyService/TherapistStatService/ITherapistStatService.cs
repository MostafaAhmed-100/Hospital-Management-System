using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.TherapistStatService
{
    public interface ITherapistStatService
    {
        Task<ApiResponseDto<IEnumerable<TherapistSpecializationDistributionDto>>> GetTherapistSpecializationDistributionAsync();
        Task<ApiResponseDto<IEnumerable<TopActiveTherapistDto>>> GetTopActiveTherapistsAsync();
    }
}
