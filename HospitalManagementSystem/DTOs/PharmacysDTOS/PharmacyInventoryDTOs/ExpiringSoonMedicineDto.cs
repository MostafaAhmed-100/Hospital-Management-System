namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs
{
    public class ExpiringSoonMedicineDto
    {
        public string MedicineName { get; set; }
        public string PharmacyName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
