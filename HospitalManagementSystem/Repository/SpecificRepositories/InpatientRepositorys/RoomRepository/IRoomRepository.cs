using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.RoomRepository
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<(RoomType RoomType, int Count)>> GetRoomsDistributionByTypeAsync();
        Task<IEnumerable<(string DepartmentName, int Count)>> GetTopDepartmentsByRoomCountAsync();
    }
}