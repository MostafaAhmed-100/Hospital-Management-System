namespace HospitalManagementSystem.DTOs.SaleItemDTOs
{
    public class CreateSaleItemDto
    {
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
