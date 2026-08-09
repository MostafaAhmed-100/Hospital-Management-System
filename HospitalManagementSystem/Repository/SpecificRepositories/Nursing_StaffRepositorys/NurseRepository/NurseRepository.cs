using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseRepository
{
    public class NurseRepository : GenericRepository<Nurse>, INurseRepository
    {
        public NurseRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
        public async Task<IEnumerable<Nurse>> GetNursesByShiftAsync(ShiftType shift)
        {
            return await _AppDbcontext.Set<Nurse>()
                .Include(n => n.Staff)
                .Where(n => n.Shift == shift)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Nurse>> GetNursesByWardAsync(string wardSpecialization)
        {
            return await _AppDbcontext.Set<Nurse>()
                .Include(n => n.Staff)
                .Where(n => n.WardSpecialization.Contains(wardSpecialization))
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<(ShiftType Shift, int Count)>> GetNursesDistributionByShiftAsync()
        {
            var distribution = await _AppDbcontext.Nurses
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Shift)
                .Select(g => new { Shift = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Shift, x.Count));
        }
        public async Task<IEnumerable<(string WardSpecialization, int Count)>> GetTopWardSpecializationsAsync()
        {
            var topWards = await _AppDbcontext.Nurses
                .Where(x => !x.IsDeleted && !string.IsNullOrEmpty(x.WardSpecialization))
                .GroupBy(x => x.WardSpecialization)
                .Select(g => new { WardSpecialization = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topWards.Select(x => (x.WardSpecialization, x.Count));
        }
    }
}