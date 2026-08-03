using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.RoomRepository
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByDepartmentIdAsync(int departmentId);
    }
}