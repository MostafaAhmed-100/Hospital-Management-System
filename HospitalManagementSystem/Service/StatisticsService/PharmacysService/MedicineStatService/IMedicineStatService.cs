using HospitalManagementSystem.DTOs.PharmacysDTOS.MedicineDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.MedicineStatService
{
    public interface IMedicineStatService
    {
        Task<ApiResponseDto<IEnumerable<TopSellingMedicineDto>>> GetTopSellingMedicinesAsync();
        Task<ApiResponseDto<IEnumerable<MedicinePrescriptionDistributionDto>>> GetMedicinePrescriptionDistributionAsync();
    }
}
