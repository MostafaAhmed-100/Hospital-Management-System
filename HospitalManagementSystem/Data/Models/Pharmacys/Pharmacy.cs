namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<PharmacySale> PharmacySales { get; set; }
        public ICollection<PharmacyInventory> PharmacyInventories { get; set; }
    }
}