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
        public async Task<IEnumerable<(LabTestStatus Status, int Count)>> GetLabTestStatusDistributionAsync()
        {
            var distribution = await _AppDbcontext.LabTests
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Status, x.Count));
        }
        
        public async Task<IEnumerable<(string TestName, int Count)>> GetTopRequestedLabTestsAsync()
        {
            var topTests = await _AppDbcontext.LabTests
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.TestName)
                .Select(g => new { TestName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topTests.Select(x => (x.TestName, x.Count));
        }
    }
}