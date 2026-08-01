using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InsuranceProviderRepository
{
    public class InsuranceProviderRepository : GenericRepository<InsuranceProvider>, IInsuranceProviderRepository
    {
        public InsuranceProviderRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<InsuranceProvider?> GetProviderWithPatientsAsync(int providerId)
        {
            var Provider = await _AppDbcontext.InsuranceProviders
                .Include(x => x.Patients)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == providerId);

            return Provider;
        }
    }
}