namespace HospitalManagementSystem.DTOs.PharmacySaleDTOs
{
    public class SaleItemDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
