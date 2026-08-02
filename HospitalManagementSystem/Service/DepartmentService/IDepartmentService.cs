using HospitalManagementSystem.DTOs.DepartmentDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.DepartmentService
{
    public interface IDepartmentService
    {
        Task<ApiResponseDto<PagedResultDto<DepartmentResponseDto>>> GetAllDepartmentsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<DepartmentWithDetailsResponseDto>> GetDepartmentByIdAsync(int id);
        Task<ApiResponseDto<DepartmentResponseDto>> CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<ApiResponseDto<string>> UpdateDepartmentAsync(UpdateDepartmentDto dto);
        Task<ApiResponseDto<string>> DeleteDepartmentAsync(int id);
    }
}