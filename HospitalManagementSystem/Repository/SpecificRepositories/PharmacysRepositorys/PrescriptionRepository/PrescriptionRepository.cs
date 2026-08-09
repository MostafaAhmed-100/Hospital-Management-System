using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionRepository
{
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Prescription?> GetPrescriptionWithItemsAsync(int prescriptionId)
        {
            var Prescription = await _AppDbcontext.Prescriptions
                .Include(x=> x.PrescriptionItems)
                .ThenInclude(x => x.Medicine)
                .AsSplitQuery()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == prescriptionId);
            return Prescription;
        }
        public async Task<IEnumerable<(PrescriptionStatus Status, int Count)>> GetPrescriptionStatusDistributionAsync()
        {
            var distribution = await _AppDbcontext.Prescriptions
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Status, x.Count));
        }

        public async Task<IEnumerable<(string DoctorName, int Count)>> GetTopPrescribingDoctorsAsync()
        {
            var topDoctors = await _AppDbcontext.Prescriptions
                .Include(x => x.Doctor)
                .GroupBy(x => x.Doctor.FullName)
                .Select(g => new { DoctorName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topDoctors.Select(x => (x.DoctorName, x.Count));
        }
    }
}
