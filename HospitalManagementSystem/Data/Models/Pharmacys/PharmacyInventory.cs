namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class PharmacyInventory
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public Medicine Medicine { get; set; }
    }
}
