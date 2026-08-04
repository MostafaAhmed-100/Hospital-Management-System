using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.BedRepository
{
    public class BedRepository : GenericRepository<Bed>, IBedRepository
    {
        public BedRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Bed>> GetAvailableBedsAsync()
        {
            return await _AppDbcontext.Beds
                .Include(b => b.Room)
                .Where(b => b.Status == BedStatus.Available)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}