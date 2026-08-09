using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PharmacySaleStatService
{
    public class PharmacySaleStatService : IPharmacySaleStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacySaleStatService> _logger;

        public PharmacySaleStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacySaleStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponseDto<IEnumerable<PharmacyRevenueDto>>> GetTotalRevenueByPharmacyAsync()
        {
            try
            {
                var revenues = await _unitOfWork.PharmacySales.GetTotalRevenueByPharmacyAsync();

                var revenueDtos = revenues.Select(d => new PharmacyRevenueDto
                {
                    PharmacyName = d.PharmacyName,
                    TotalRevenue = d.TotalRevenue
                }).ToList();

                return new ApiResponseDto<IEnumerable<PharmacyRevenueDto>>
                {
                    IsSuccess = true,
                    Message = "Total revenue by pharmacy retrieved successfully.",
                    StatusCode = 200,
                    Data = revenueDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving total revenue by pharmacy.");
                throw;
            }
        }
        public async Task<ApiResponseDto<IEnumerable<SalePrescriptionDistributionDto>>> GetSalesDistributionByPrescriptionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.PharmacySales.GetSalesDistributionByPrescriptionAsync();

                var distributionDtos = distribution.Select(d => new SalePrescriptionDistributionDto
                {
                    Category = d.Category,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<SalePrescriptionDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Sales distribution by prescription retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving sales distribution by prescription.");
                throw;
            }
        }
    }
}
