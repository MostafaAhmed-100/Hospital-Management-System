using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.InpatientService.AdmissionService
{
    public interface IAdmissionService
    {
        Task<ApiResponseDto<PagedResultDto<AdmissionResponseDto>>> GetAllAdmissionsAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<AdmissionResponseDto>> GetAdmissionByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<AdmissionResponseDto>>> GetActiveAdmissionsAsync();
        Task<ApiResponseDto<AdmissionResponseDto>> CreateAdmissionAsync(CreateAdmissionDto dto);
        Task<ApiResponseDto<string>> UpdateAdmissionAsync(UpdateAdmissionDto dto);
        Task<ApiResponseDto<string>> DischargePatientAsync(int id);
        Task<ApiResponseDto<string>> DeleteAdmissionAsync(int id);
    }
}
