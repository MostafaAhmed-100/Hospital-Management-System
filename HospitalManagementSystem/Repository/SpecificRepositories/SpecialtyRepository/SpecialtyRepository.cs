using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SpecialtyRepository
{
    public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Specialty?> GetSpecialtyWithDoctorsAsync(int specialtyId)
        {
            var Specialty = await _AppDbcontext.Specialties
                .Include(x => x.Doctors)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == specialtyId);
            return Specialty;
        }
    }
}
