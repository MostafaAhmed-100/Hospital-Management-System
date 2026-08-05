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
    }
}
