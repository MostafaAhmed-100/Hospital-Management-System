using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Billing_Insurance
{
    public class InsuranceProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public int CoveragePercentage { get; set; }
        public ICollection <Patient> Patients { get; set; }
    }
}
