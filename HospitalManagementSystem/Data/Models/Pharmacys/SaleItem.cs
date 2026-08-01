namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public PharmacySale PharmacySale { get; set; }
        public Medicine Medicine { get; set; }
    }
}
