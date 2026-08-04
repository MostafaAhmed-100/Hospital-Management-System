using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionItemRepository
{
    public class PrescriptionItemRepository : GenericRepository<PrescriptionItem>, IPrescriptionItemRepository
    {
        public PrescriptionItemRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(int prescriptionId)
        {
            var items = await _AppDbcontext.PrescriptionItems
                .Include(x => x.Medicine)
                .Where(x => x.PrescriptionId == prescriptionId)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }
    }
}