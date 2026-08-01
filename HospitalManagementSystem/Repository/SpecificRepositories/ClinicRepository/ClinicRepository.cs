using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.ClinicRepository
{
    public class ClinicRepository : GenericRepository<Clinic>, IClinicRepository
    {
        public ClinicRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Clinic?>> GetClinicsByDepartmentAsync(int departmentId)
        {
            var Clinics = await _AppDbcontext.Clinics
                .Where(x => x.DepartmentId == departmentId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
            return Clinics;
        }
    }
}
