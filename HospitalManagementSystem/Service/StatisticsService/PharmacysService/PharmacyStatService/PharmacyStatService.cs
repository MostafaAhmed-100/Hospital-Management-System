using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacyStatService
{
    public class PharmacyStatService : IPharmacyStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacyStatService> _logger;

        public PharmacyStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacyStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopPharmacyBySalesDto>>> GetTopPharmaciesBySalesCountAsync()
        {
            try
            {
                var topPharmacies = await _unitOfWork.Pharmacies.GetTopPharmaciesBySalesCountAsync();

                var pharmacyDtos = topPharmacies.Select(d => new TopPharmacyBySalesDto
                {
                    PharmacyName = d.PharmacyName,
                    SalesCount = d.SalesCount
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopPharmacyBySalesDto>>
                {
                    IsSuccess = true,
                    Message = "Top pharmacies by sales count retrieved successfully.",
                    StatusCode = 200,
                    Data = pharmacyDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top pharmacies by sales count.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TopPharmacyByInventoryDto>>> GetTopPharmaciesByInventorySizeAsync()
        {
            try
            {
                var topInventories = await _unitOfWork.Pharmacies.GetTopPharmaciesByInventorySizeAsync();

                var inventoryDtos = topInventories.Select(d => new TopPharmacyByInventoryDto
                {
                    PharmacyName = d.PharmacyName,
                    InventoryCount = d.InventoryCount
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopPharmacyByInventoryDto>>
                {
                    IsSuccess = true,
                    Message = "Top pharmacies by inventory size retrieved successfully.",
                    StatusCode = 200,
                    Data = inventoryDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top pharmacies by inventory size.");
                throw;
            }
        }
    }
}
