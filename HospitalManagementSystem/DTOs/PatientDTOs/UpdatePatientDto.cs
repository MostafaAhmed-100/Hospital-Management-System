namespace HospitalManagementSystem.DTOs.PatientDTOs
{
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? InsuranceId { get; set; }
    }
}
