using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.RoomRepository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Room>> GetRoomsByDepartmentIdAsync(int departmentId)
        {
            return await _AppDbcontext.Set<Room>()
                .Include(r => r.Beds)
                .Where(r => r.DepartmentId == departmentId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}