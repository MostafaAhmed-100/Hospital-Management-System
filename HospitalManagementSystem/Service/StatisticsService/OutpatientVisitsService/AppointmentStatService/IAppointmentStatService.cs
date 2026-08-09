using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.AppointmentStatService
{
    public interface IAppointmentStatService
    {
        Task<ApiResponseDto<IEnumerable<AppointmentStatusDistributionDto>>> GetAppointmentsDistributionByStatusAsync();
        Task<ApiResponseDto<int>> GetTodayAppointmentsCountAsync();
    }
}
