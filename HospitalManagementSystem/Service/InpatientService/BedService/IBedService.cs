using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.InpatientService.BedService
{
    public interface IBedService
    {
        Task<ApiResponseDto<PagedResultDto<BedResponseDto>>> GetAllBedsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<BedResponseDto>> GetBedByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<BedResponseDto>>> GetAvailableBedsAsync();
        Task<ApiResponseDto<BedResponseDto>> CreateBedAsync(CreateBedDto dto);
        Task<ApiResponseDto<string>> UpdateBedAsync(UpdateBedDto dto);
        Task<ApiResponseDto<string>> DeleteBedAsync(int id);
    }
}
