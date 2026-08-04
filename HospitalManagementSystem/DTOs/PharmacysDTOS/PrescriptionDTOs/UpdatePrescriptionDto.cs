using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs
{
    public class UpdatePrescriptionDto
    {
        public int Id { get; set; }
        public PrescriptionStatus Status { get; set; }
    }
}
