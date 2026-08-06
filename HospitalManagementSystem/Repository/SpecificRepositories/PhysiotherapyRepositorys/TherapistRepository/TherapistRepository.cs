using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.TherapistRepository
{
    public class TherapistRepository : GenericRepository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Therapist>> GetTherapistsByDepartmentIdAsync(int departmentId)
        {
            return await _AppDbcontext.Set<Therapist>()
                .Include(t => t.Department)
                .Where(t => t.DepartmentId == departmentId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
