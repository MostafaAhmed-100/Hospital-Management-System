namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionItemDTOs
{
    public class UpdatePrescriptionItemDto
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }
}
