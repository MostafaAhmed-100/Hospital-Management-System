using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PrescriptionStatService
{
    public interface IPrescriptionStatService
    {
        Task<ApiResponseDto<IEnumerable<PrescriptionStatusDistributionDto>>> GetPrescriptionStatusDistributionAsync();
        Task<ApiResponseDto<IEnumerable<TopPrescribingDoctorDto>>> GetTopPrescribingDoctorsAsync();
    }
}