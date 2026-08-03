using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.StaffRepository
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
    }
}
