using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.DoctorDTOs
{
    public class CreateDoctorDto
    {
        public String FullName { get; set; }
        public DoctorType DoctorType { get; set; }
        public decimal ConsultationFee { get; set; }
        public int DepartmentId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
