using HospitalManagementSystem.DTOs.ClinicDTOs;
using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.Clinics_DoctorsService
{
    public interface IClinicsAndDoctorsStat
    {
        Task<ApiResponseDto<IEnumerable<ClinicResponseDto>>> GetTheMostClinicsWithAppointmentInDepartment(int departmentId);
        Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTheMostDoctorsWithAppointmentsInDepartment(int departmentId);
        Task<ApiResponseDto<IEnumerable<ClinicResponseDto>>> GetTheMostClinicsWithAppointmentInHospital();
        Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTheMostDoctorsWithAppointmentsInHospital();
    }
}
