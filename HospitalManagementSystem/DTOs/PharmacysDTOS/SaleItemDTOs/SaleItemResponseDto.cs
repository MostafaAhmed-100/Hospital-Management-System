namespace HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs
{
    public class SaleItemResponseDto
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
