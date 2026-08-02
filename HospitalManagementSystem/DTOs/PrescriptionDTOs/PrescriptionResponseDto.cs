using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.PrescriptionDTOs
{
    public class PrescriptionResponseDto
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public PrescriptionStatus Status { get; set; }
    }
}
