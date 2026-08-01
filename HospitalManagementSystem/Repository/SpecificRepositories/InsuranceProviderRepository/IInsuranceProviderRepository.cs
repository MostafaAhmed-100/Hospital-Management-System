using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InsuranceProviderRepository
{
    public interface IInsuranceProviderRepository : IGenericRepository<InsuranceProvider>
    {
        Task<InsuranceProvider?> GetProviderWithPatientsAsync(int providerId);
    }
}