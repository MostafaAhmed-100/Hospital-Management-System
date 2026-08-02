namespace HospitalManagementSystem.DTOs.PrescriptionItemDTOs
{
    public class PrescriptionItemResponseDto
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }
}
