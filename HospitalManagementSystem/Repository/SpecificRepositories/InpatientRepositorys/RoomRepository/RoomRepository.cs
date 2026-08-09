using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
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
        public async Task<IEnumerable<(RoomType RoomType, int Count)>> GetRoomsDistributionByTypeAsync()
        {
            var distribution = await _AppDbcontext.Rooms
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.RoomType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Type, x.Count));
        }

        public async Task<IEnumerable<(string DepartmentName, int Count)>> GetTopDepartmentsByRoomCountAsync()
        {
            var topDepartments = await _AppDbcontext.Rooms
                .Where(x => !x.IsDeleted)
                .Include(x => x.Department)
                .GroupBy(x => x.Department.Name)
                .Select(g => new { DepartmentName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topDepartments.Select(x => (x.DepartmentName, x.Count));
        }
    }
}