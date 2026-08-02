using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.DoctorService
{
    public interface IDoctorService
    {
        Task<ApiResponseDto<PagedResultDto<DoctorResponseDto>>> GetAllDoctorsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<DoctorResponseDto>> GetDoctorByIdAsync(int id);
        Task<ApiResponseDto<DoctorResponseDto>> CreateDoctorAsync(CreateDoctorDto dto);
        Task<ApiResponseDto<string>> UpdateDoctorAsync(UpdateDoctorDto dto);
        Task<ApiResponseDto<string>> DeleteDoctorAsync(int id);
    }
}