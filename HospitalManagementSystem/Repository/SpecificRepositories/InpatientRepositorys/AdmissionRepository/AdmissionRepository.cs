using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.AdmissionRepository
{
    public class AdmissionRepository : GenericRepository<Admission>, IAdmissionRepository
    {
        public AdmissionRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Admission>> GetActiveAdmissionsAsync()
        {
            return await _AppDbcontext.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Bed)
                .Include(a => a.Doctor)
                .Where(a => a.Status == AdmissionStatus.Active && a.DischargeDate == null)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<Admission?> GetActiveAdmissionByBedIdAsync(int bedId)
        {
            return await _AppDbcontext.Admissions
                .FirstOrDefaultAsync(a => a.BedId == bedId && a.Status == AdmissionStatus.Active && a.DischargeDate == null);
        }
        public async Task<int> GetActiveAdmissionsCountAsync()
        {
            var count = await _AppDbcontext.Admissions
                .Where(x => x.Status == AdmissionStatus.Active)
                .CountAsync();

            return count;
        }
        public async Task<IEnumerable<Doctor?>> GetTopAdmittingDoctorsAsync()
        {
            var doctors = await _AppDbcontext.Admissions
                .Include(x => x.Doctor)
                .GroupBy(x => x.Doctor)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(5)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return doctors;
        }
    }
}