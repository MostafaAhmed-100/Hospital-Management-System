using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyInventoryStatService
{
    public interface IPharmacyInventoryStatService
    {
        Task<ApiResponseDto<IEnumerable<ExpiringSoonMedicineDto>>> GetExpiringSoonMedicinesAsync();
        Task<ApiResponseDto<IEnumerable<LowStockMedicineDto>>> GetLowStockMedicinesAsync();
    }
}
