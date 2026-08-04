using HospitalManagementSystem.DTOs.ClinicDTOs;
using HospitalManagementSystem.DTOs.DoctorDTOs;

namespace HospitalManagementSystem.DTOs.DepartmentDTOs
{
    public class DepartmentWithDetailsResponseDto : DepartmentResponseDto
    {
        public IEnumerable<ClinicResponseDto> Clinics {  get; set; }

        public IEnumerable<DoctorResponseDto> Doctors {  get; set; }
    }
}
