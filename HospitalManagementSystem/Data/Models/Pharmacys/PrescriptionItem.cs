namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class PrescriptionItem
    {
        public int Id { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
        public int MedicineId { get; set; }
        public int PrescriptionId { get; set; }
        public Medicine Medicine { get; set; }
        public Prescription Prescription { get; set; }
    }
}
