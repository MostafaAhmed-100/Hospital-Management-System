using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.OperatingRoomRepository
{
    public class OperatingRoomRepository : GenericRepository<OperatingRoom>, IOperatingRoomRepository
    {
        public OperatingRoomRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<OperatingRoom>> GetAvailableOperatingRoomsAsync()
        {
            return await _AppDbcontext.OperatingRooms
                .Where(or => or.Status == OperatingRoomStatus.Available)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
