namespace HospitalManagementSystem.DTOs.MedicineDTOs
{
    public class CreateMedicineDto
    {
        public string Name { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
