using HospitalManagementSystem.DTOs.LabTestDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.LabTestService
{
    public interface ILabTestStatService
    {
        Task<ApiResponseDto<IEnumerable<LabTestStatusDistributionDto>>> GetLabTestStatusDistributionAsync();
        Task<ApiResponseDto<IEnumerable<TopLabTestDto>>> GetTopRequestedLabTestsAsync();
    }
}
