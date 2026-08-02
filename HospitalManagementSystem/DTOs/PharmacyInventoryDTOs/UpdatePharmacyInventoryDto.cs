namespace HospitalManagementSystem.DTOs.PharmacyInventoryDTOs
{
    public class UpdatePharmacyInventoryDto
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
