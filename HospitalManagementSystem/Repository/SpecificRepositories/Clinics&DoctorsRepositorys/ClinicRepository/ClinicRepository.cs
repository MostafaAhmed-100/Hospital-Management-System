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

        public async Task<IEnumerable<Clinic?>> GetTheMostClinicsWithAppointmentInDepartment(int departmentId)
        {
            var Clinics = await _AppDbcontext.Clinics
                .Where(x => x.DepartmentId == departmentId)
                .AsNoTrackingWithIdentityResolution()
                .OrderByDescending(x => x.Appointments.Count)
                .Take(5)
                .ToListAsync();

            return Clinics;
        }

        public async Task<IEnumerable<Clinic?>> GetTheMostClinicsWithAppointmentInHospital()
        {
            var Clinics = await _AppDbcontext.Clinics
               .AsNoTrackingWithIdentityResolution()
               .OrderByDescending(x => x.Appointments.Count)
               .Take(5)
               .ToListAsync();

            return Clinics;
        }
    }
}
