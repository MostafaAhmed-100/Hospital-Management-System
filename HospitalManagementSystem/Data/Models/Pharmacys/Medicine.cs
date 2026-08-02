namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<PrescriptionItem> PrescriptionItems { get; set; }
        public ICollection<PharmacyInventory> PharmacyInventories { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
