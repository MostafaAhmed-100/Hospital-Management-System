namespace HospitalManagementSystem.DTOs.PharmacySaleDTOs
{
    public class PharmacySaleResponseDto
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int? PrescriptionId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
