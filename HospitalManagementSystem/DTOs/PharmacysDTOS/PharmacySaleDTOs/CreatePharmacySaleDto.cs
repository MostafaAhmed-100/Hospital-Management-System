namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs
{
    public class CreatePharmacySaleDto
    {
        public int PharmacyId { get; set; }
        public int PatientId { get; set; }
        public int? PrescriptionId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
