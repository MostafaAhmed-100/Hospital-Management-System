using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;

namespace HospitalManagementSystem.Service.NursingStaffService.NurseService
{
    public interface INurseService
    {
        Task<ApiResponseDto<PagedResultDto<NurseResponseDto>>> GetAllNursesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<NurseResponseDto>> GetNurseByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<NurseResponseDto>>> GetNursesByShiftAsync(ShiftType shift);
        Task<ApiResponseDto<IEnumerable<NurseResponseDto>>> GetNursesByWardAsync(string wardSpecialization);
        Task<ApiResponseDto<NurseResponseDto>> CreateNurseAsync(CreateNurseDto dto);
        Task<ApiResponseDto<string>> UpdateNurseAsync(UpdateNurseDto dto);
        Task<ApiResponseDto<string>> DeleteNurseAsync(int id);
    }
}