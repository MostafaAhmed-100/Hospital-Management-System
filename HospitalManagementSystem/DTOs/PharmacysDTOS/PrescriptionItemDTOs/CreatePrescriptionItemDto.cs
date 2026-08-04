namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionItemDTOs
{
    public class CreatePrescriptionItemDto
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }
}
