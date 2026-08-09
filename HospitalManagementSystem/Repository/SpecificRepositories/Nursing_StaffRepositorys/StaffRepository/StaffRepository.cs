using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.StaffRepository
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Staff>> GetStaffByClinicIdAsync(int clinicId)
        {
            return await _AppDbcontext.Set<Staff>()
                .Include(s => s.Clinic)
                .Where(s => s.ClinicId == clinicId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetStaffByRoleAsync(StaffRole role)
        {
            return await _AppDbcontext.Set<Staff>()
                .Include(s => s.Clinic)
                .Where(s => s.Role == role)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<(StaffRole Role, int Count)>> GetStaffDistributionByRoleAsync()
        {
            var distribution = await _AppDbcontext.Staff
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Role)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Role, x.Count));
        }

        public async Task<IEnumerable<(string ClinicName, int Count)>> GetTopClinicsByStaffCountAsync()
        {
            var topClinics = await _AppDbcontext.Staff
                .Where(x => !x.IsDeleted)
                .Include(x => x.Clinic)
                .GroupBy(x => x.Clinic.Name)
                .Select(g => new { ClinicName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topClinics.Select(x => (x.ClinicName, x.Count));
        }
    }
}