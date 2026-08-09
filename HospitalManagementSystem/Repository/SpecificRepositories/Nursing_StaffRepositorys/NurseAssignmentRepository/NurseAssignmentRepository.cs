using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseAssignmentRepository
{
    public class NurseAssignmentRepository : GenericRepository<NurseAssignment>, INurseAssignmentRepository
    {
        public NurseAssignmentRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<NurseAssignment>> GetAssignmentsByNurseIdAsync(int nurseId)
        {
            return await _AppDbcontext.Set<NurseAssignment>()
                .Include(n => n.Nurse).ThenInclude(nu => nu.Staff)
                .Include(n => n.Admission)
                .Include(n => n.ErVisit)
                .Where(n => n.NurseId == nurseId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<NurseAssignment>> GetAssignmentsByAdmissionIdAsync(int admissionId)
        {
            return await _AppDbcontext.Set<NurseAssignment>()
                .Include(n => n.Nurse).ThenInclude(nu => nu.Staff)
                .Include(n => n.Admission)
                .Where(n => n.AdmissionId == admissionId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<NurseAssignment>> GetAssignmentsByErVisitIdAsync(int erVisitId)
        {
            return await _AppDbcontext.Set<NurseAssignment>()
                .Include(n => n.Nurse).ThenInclude(nu => nu.Staff)
                .Include(n => n.ErVisit)
                .Where(n => n.ErVisitId == erVisitId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<(string LicenseNumber, int Count)>> GetTopAssignedNursesAsync()
        {
            var topNurses = await _AppDbcontext.NurseAssignments
                .Where(x => !x.IsDeleted)
                .Include(x => x.Nurse)
                .GroupBy(x => x.Nurse.LicenseNumber)
                .Select(g => new { LicenseNumber = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topNurses.Select(x => (x.LicenseNumber, x.Count));
        }

        public async Task<IEnumerable<(ShiftType Shift, int Count)>> GetAssignmentsDistributionByShiftAsync()
        {
            var distribution = await _AppDbcontext.NurseAssignments
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Shift)
                .Select(g => new { Shift = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Shift, x.Count));
        }
    }
}