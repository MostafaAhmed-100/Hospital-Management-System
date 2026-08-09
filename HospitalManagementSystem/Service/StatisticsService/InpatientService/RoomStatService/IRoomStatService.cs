using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.RoomStatService
{
    public interface IRoomStatService
    {
        Task<ApiResponseDto<IEnumerable<RoomTypeDistributionDto>>> GetRoomsDistributionByTypeAsync();
        Task<ApiResponseDto<IEnumerable<DepartmentRoomCountDto>>> GetTopDepartmentsByRoomCountAsync();
    }
}
