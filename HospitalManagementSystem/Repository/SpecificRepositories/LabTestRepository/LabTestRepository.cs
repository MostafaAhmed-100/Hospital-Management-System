using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.LabTestRepository
{
    public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<LabTest>> GetTestsByRecordIdAsync(int recordId)
        {
            return await _AppDbcontext.Set<LabTest>()
                .Where(l => l.RecordId == recordId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<LabTest>> GetPendingTestsAsync()
        {
            return await _AppDbcontext.Set<LabTest>()
                .Where(l => l.Status == LabTestStatus.Pending)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
