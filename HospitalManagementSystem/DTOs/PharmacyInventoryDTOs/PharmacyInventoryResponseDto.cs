namespace HospitalManagementSystem.DTOs.PharmacyInventoryDTOs
{
    public class PharmacyInventoryResponseDto
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
