using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SpecialtyDTOs;

namespace HospitalManagementSystem.Service.SpecialtyService
{
    public interface ISpecialtyService
    {
        Task<ApiResponseDto<PagedResultDto<SpecialtyResponseDto>>> GetAllSpecialtiesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<SpecialtyWithDoctorsResponseDto>> GetSpecialtyByIdAsync(int id);
        Task<ApiResponseDto<SpecialtyResponseDto>> CreateSpecialtyAsync(CreateSpecialtyDto dto);
        Task<ApiResponseDto<string>> UpdateSpecialtyAsync(UpdateSpecialtyDto dto);
        Task<ApiResponseDto<string>> DeleteSpecialtyAsync(int id);
    }
}