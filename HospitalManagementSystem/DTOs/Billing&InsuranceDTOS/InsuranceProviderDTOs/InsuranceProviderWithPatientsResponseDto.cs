namespace HospitalManagementSystem.DTOs.InsuranceProviderDTOs
{
    public class InsuranceProviderWithPatientsResponseDto : InsuranceProviderResponseDto
    {
        public IEnumerable<ProviderPatientDto> Patients { get; set; }
    }
}
