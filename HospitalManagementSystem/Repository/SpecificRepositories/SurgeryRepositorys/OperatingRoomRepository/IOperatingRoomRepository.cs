using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.OperatingRoomRepository
{
    public interface IOperatingRoomRepository : IGenericRepository<OperatingRoom> 
    {
        Task<IEnumerable<OperatingRoom>> GetAvailableOperatingRoomsAsync();
    }
}
