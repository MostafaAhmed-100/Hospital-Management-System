namespace HospitalManagementSystem.DTOs.PharmacyInventoryDTOs
{
    public class CreatePharmacyInventoryDto
    {
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
