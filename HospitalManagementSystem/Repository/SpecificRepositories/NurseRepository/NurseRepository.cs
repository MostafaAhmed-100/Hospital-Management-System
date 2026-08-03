using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.NurseRepository
{
    public class NurseRepository : GenericRepository<Nurse>, INurseRepository
    {
        public NurseRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
    }
}
