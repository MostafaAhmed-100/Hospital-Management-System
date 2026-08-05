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
    }
}
