namespace HospitalManagementSystem.DTOs.PatientDTOs
{
    public class PatientResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? InsuranceId { get; set; }
        public string InsuranceProviderName { get; set; }
    }
}
