using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository
{
    public class OperatingRoomRepository : GenericRepository<OperatingRoom>, IOperatingRoomRepository
    {
        public OperatingRoomRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
    }
}
