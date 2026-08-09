using HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.SaleItemStatService
{
    public interface ISaleItemStatService
    {
        Task<ApiResponseDto<IEnumerable<TopRevenueMedicineDto>>> GetTopRevenueGeneratingMedicinesAsync();
    }
}
