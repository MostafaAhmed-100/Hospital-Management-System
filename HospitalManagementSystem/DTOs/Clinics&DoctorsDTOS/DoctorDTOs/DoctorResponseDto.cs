using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.DoctorDTOs
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public DoctorType DoctorType { get; set; }
        public decimal ConsultationFee { get; set; }
        public int DepartmentId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
