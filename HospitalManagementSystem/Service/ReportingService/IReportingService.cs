using HospitalManagementSystem.DTOs.ReportingDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.ReportingService
{
    public interface IReportingService
    {
        Task<ApiResponseDto<DashboardReportDto>> GetFullDashboardSummaryAsync(DateTime? startDate, DateTime? endDate);
    }
}
