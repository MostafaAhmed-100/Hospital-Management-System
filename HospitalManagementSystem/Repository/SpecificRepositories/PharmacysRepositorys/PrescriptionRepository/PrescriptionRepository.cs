using HospitalManagementSystem.Data;
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
    }
}
