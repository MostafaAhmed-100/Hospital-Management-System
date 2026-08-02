namespace HospitalManagementSystem.DTOs.PharmacyDTOs
{
    public class PharmacyInventoryDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
    }
}
