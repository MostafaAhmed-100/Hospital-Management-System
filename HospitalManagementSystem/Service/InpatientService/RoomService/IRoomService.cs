using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.InpatientService.RoomService
{
    public interface IRoomService
    {
        Task<ApiResponseDto<PagedResultDto<RoomResponseDto>>> GetAllRoomsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<RoomResponseDto>> GetRoomByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<RoomResponseDto>>> GetRoomsByDepartmentIdAsync(int departmentId);
        Task<ApiResponseDto<RoomResponseDto>> CreateRoomAsync(CreateRoomDto dto);
        Task<ApiResponseDto<string>> UpdateRoomAsync(UpdateRoomDto dto);
        Task<ApiResponseDto<string>> DeleteRoomAsync(int id);
    }
}