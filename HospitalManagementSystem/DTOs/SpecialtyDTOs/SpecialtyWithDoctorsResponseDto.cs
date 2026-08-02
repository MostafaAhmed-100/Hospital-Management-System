using HospitalManagementSystem.DTOs.DoctorDTOs;

namespace HospitalManagementSystem.DTOs.SpecialtyDTOs
{
    public class SpecialtyWithDoctorsResponseDto : SpecialtyResponseDto
    {
        public IEnumerable<DoctorResponseDto> Doctors { get; set; }
    }
}
