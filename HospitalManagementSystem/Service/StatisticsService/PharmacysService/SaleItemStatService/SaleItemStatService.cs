using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.SaleItemStatService
{
    public class SaleItemStatService : ISaleItemStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleItemStatService> _logger;

        public SaleItemStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SaleItemStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopRevenueMedicineDto>>> GetTopRevenueGeneratingMedicinesAsync()
        {
            try
            {
                var topMedicines = await _unitOfWork.SaleItems.GetTopRevenueGeneratingMedicinesAsync();

                var medicineDtos = topMedicines.Select(d => new TopRevenueMedicineDto
                {
                    MedicineName = d.MedicineName,
                    TotalRevenue = d.TotalRevenue
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopRevenueMedicineDto>>
                {
                    IsSuccess = true,
                    Message = "Top revenue-generating medicines retrieved successfully.",
                    StatusCode = 200,
                    Data = medicineDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top revenue-generating medicines.");
                throw;
            }
        }
    }
}