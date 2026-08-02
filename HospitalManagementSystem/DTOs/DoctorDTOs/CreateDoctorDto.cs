using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.DoctorDTOs
{
    public class CreateDoctorDto
    {
        public DoctorType DoctorType { get; set; }
        public decimal ConsultationFee { get; set; }
        public int DepartmentId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
