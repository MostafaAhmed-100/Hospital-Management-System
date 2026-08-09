using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DoctorRepository
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Doctor?>> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            var Doctors = await _AppDbcontext.Doctors
                .Include(x => x.Specialty)
                .Where(x =>  x.SpecialtyId == specialtyId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
            return Doctors;
        }

        public async Task<Doctor?> GetDoctorWithDepartmentAsync(int doctorId)
        {
            var Doctor = await _AppDbcontext.Doctors
                .Include(x => x.Department)
                .Include(x => x.Specialty)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == doctorId);
            return Doctor;
        }

        public async Task<IEnumerable<Doctor?>> GetTheMostDoctorsWithAppointmentsInDepartment(int departmentId)
        {
            var Doctors = await _AppDbcontext.Doctors
                .Where(x => x.DepartmentId == departmentId)
                .AsNoTrackingWithIdentityResolution()
                .OrderByDescending(x => x.Appointments.Count)
                .Take(5)
                .ToListAsync();

            return Doctors;
        }

        public async Task<IEnumerable<Doctor?>> GetTheMostDoctorsWithAppointmentsInHospital()
        {
            var Doctors = await _AppDbcontext.Doctors
             .AsNoTrackingWithIdentityResolution()
             .OrderByDescending(x => x.Appointments.Count)
             .Take(5)
             .ToListAsync();

            return Doctors;
        }
    }
}
