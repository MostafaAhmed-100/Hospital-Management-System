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
    }
}
