using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseAssignmentRepository
{
    public class NurseAssignmentRepository : GenericRepository<NurseAssignment>, INurseAssignmentRepository
    {
        public NurseAssignmentRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
    }
}
