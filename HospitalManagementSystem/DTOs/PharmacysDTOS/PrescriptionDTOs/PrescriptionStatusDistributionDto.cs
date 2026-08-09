using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs
{
    public class PrescriptionStatusDistributionDto
    {
        public PrescriptionStatus Status { get; set; }
        public int Count { get; set; }
    }
}
