using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;

namespace HospitalManagementSystem.Service.NursingStaffService.StaffService
{
    public interface IStaffService
    {
        Task<ApiResponseDto<PagedResultDto<StaffResponseDto>>> GetAllStaffAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<StaffResponseDto>> GetStaffByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<StaffResponseDto>>> GetStaffByClinicIdAsync(int clinicId);
        Task<ApiResponseDto<IEnumerable<StaffResponseDto>>> GetStaffByRoleAsync(StaffRole role);
        Task<ApiResponseDto<StaffResponseDto>> CreateStaffAsync(CreateStaffDto dto);
        Task<ApiResponseDto<string>> UpdateStaffAsync(UpdateStaffDto dto);
        Task<ApiResponseDto<string>> DeleteStaffAsync(int id);
    }
}