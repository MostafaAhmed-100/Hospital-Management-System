namespace HospitalManagementSystem.DTOs.PharmacysDTOS.MedicineDTOs
{
    public class MedicineResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
