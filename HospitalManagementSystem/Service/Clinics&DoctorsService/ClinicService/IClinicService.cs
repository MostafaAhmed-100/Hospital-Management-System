using HospitalManagementSystem.DTOs.ClinicDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.ClinicService
{
    public interface IClinicService
    {
        Task<ApiResponseDto<PagedResultDto<ClinicResponseDto>>> GetAllClinicsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<ClinicResponseDto>> GetClinicByIdAsync(int id);
        Task<ApiResponseDto<ClinicResponseDto>> CreateClinicAsync(CreateClinicDto dto);
        Task<ApiResponseDto<string>> UpdateClinicAsync(UpdateClinicDto dto);
        Task<ApiResponseDto<string>> DeleteClinicAsync(int id);
    }
}
