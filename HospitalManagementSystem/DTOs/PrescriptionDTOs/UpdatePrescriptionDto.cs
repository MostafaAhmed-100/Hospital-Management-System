using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.PrescriptionDTOs
{
    public class UpdatePrescriptionDto
    {
        public int Id { get; set; }
        public PrescriptionStatus Status { get; set; }
    }
}
