using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;

namespace HospitalManagementSystem.Service.SurgeryService.OperatingRoomService
{
    public interface IOperatingRoomService
    {
        Task<ApiResponseDto<PagedResultDto<OperatingRoomResponseDto>>> GetAllOperatingRoomsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<OperatingRoomResponseDto>> GetOperatingRoomByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<OperatingRoomResponseDto>>> GetAvailableOperatingRoomsAsync();
        Task<ApiResponseDto<OperatingRoomResponseDto>> CreateOperatingRoomAsync(CreateOperatingRoomDto dto);
        Task<ApiResponseDto<string>> UpdateOperatingRoomAsync(UpdateOperatingRoomDto dto);
        Task<ApiResponseDto<string>> DeleteOperatingRoomAsync(int id);
    }
}
