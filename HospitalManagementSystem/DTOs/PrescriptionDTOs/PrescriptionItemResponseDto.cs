namespace HospitalManagementSystem.DTOs.PrescriptionDTOs
{
    public class PrescriptionItemResponseDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
    }
}
