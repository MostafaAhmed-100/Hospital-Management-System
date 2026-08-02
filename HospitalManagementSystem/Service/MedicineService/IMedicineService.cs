using HospitalManagementSystem.DTOs.MedicineDTOs;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Service.MedicineService
{
    public interface IMedicineService
    {
        Task<ApiResponseDto<PagedResultDto<MedicineResponseDto>>> GetAllMedicinesAsync(int pageNumber, int pageSize);
        Task<ApiResponseDto<MedicineResponseDto>> GetMedicineByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<MedicineResponseDto>>> SearchMedicinesByNameAsync(string name);
        Task<ApiResponseDto<MedicineResponseDto>> CreateMedicineAsync(CreateMedicineDto dto);
        Task<ApiResponseDto<string>> UpdateMedicineAsync(UpdateMedicineDto dto);
        Task<ApiResponseDto<string>> DeleteMedicineAsync(int id);
    }
}