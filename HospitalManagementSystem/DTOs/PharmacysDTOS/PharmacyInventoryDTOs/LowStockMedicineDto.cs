namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs
{
    public class LowStockMedicineDto
    {
        public string MedicineName { get; set; }
        public string PharmacyName { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
