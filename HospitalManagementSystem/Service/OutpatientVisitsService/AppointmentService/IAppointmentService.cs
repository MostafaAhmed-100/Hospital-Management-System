using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.OutpatientVisitsService.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ApiResponseDto<PagedResultDto<AppointmentResponseDto>>> GetAllAppointmentsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<AppointmentResponseDto>> GetAppointmentByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<AppointmentResponseDto>>> GetUpcomingAppointmentsByDoctorAsync(int doctorId);
        Task<ApiResponseDto<AppointmentResponseDto>> CreateAppointmentAsync(CreateAppointmentDto dto);
        Task<ApiResponseDto<string>> UpdateAppointmentAsync(UpdateAppointmentDto dto);
        Task<ApiResponseDto<string>> DeleteAppointmentAsync(int id);
    }
}