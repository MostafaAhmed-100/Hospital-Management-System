using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyInventoryStatService
{
    public class PharmacyInventoryStatService : IPharmacyInventoryStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacyInventoryStatService> _logger;

        public PharmacyInventoryStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacyInventoryStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<LowStockMedicineDto>>> GetLowStockMedicinesAsync()
        {
            try
            {
                var lowStock = await _unitOfWork.PharmacyInventories.GetLowStockMedicinesAsync();

                var lowStockDtos = lowStock.Select(d => new LowStockMedicineDto
                {
                    MedicineName = d.MedicineName,
                    PharmacyName = d.PharmacyName,
                    CurrentQuantity = d.Quantity
                }).ToList();

                return new ApiResponseDto<IEnumerable<LowStockMedicineDto>>
                {
                    IsSuccess = true,
                    Message = "Low stock medicines retrieved successfully.",
                    StatusCode = 200,
                    Data = lowStockDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving low stock medicines.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ExpiringSoonMedicineDto>>> GetExpiringSoonMedicinesAsync()
        {
            try
            {
                var expiringSoon = await _unitOfWork.PharmacyInventories.GetExpiringSoonMedicinesAsync();

                var expiringDtos = expiringSoon.Select(d => new ExpiringSoonMedicineDto
                {
                    MedicineName = d.MedicineName,
                    PharmacyName = d.PharmacyName,
                    ExpiryDate = d.ExpiryDate,
                    RemainingQuantity = d.Quantity
                }).ToList();

                return new ApiResponseDto<IEnumerable<ExpiringSoonMedicineDto>>
                {
                    IsSuccess = true,
                    Message = "Expiring soon medicines retrieved successfully.",
                    StatusCode = 200,
                    Data = expiringDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving expiring soon medicines.");
                throw;
            }
        }
    }
}
