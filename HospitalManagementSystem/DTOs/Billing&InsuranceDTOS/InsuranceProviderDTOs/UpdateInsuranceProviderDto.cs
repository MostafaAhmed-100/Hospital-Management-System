namespace HospitalManagementSystem.DTOs.InsuranceProviderDTOs
{
    public class UpdateInsuranceProviderDto
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public int CoveragePercentage { get; set; }
    }
}
