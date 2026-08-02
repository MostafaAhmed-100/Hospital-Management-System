namespace HospitalManagementSystem.DTOs.PharmacySaleDTOs
{
    public class UpdatePharmacySaleDto
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int PatientId { get; set; }
        public int? PrescriptionId { get; set; }
        public decimal TotalAmount { get; set; }
    
    }
}
