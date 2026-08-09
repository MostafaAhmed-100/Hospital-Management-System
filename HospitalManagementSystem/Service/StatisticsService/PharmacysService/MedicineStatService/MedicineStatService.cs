using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.MedicineDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.MedicineStatService
{
    public class MedicineStatService : IMedicineStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicineStatService> _logger;

        public MedicineStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<MedicineStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopSellingMedicineDto>>> GetTopSellingMedicinesAsync()
        {
            try
            {
                var topMedicines = await _unitOfWork.Medicines.GetTopSellingMedicinesAsync();

                var medicineDtos = topMedicines.Select(d => new TopSellingMedicineDto
                {
                    MedicineName = d.MedicineName,
                    SalesCount = d.SalesCount
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopSellingMedicineDto>>
                {
                    IsSuccess = true,
                    Message = "Top selling medicines retrieved successfully.",
                    StatusCode = 200,
                    Data = medicineDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top selling medicines.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<MedicinePrescriptionDistributionDto>>> GetMedicinePrescriptionDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Medicines.GetMedicinePrescriptionDistributionAsync();

                var distributionDtos = distribution.Select(d => new MedicinePrescriptionDistributionDto
                {
                    Category = d.Category,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<MedicinePrescriptionDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Medicine prescription distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving medicine prescription distribution.");
                throw;
            }
        }
    }
}
