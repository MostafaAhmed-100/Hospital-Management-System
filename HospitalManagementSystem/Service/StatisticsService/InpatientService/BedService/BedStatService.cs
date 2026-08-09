using AutoMapper;
using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.BedService
{
    public class BedStatService : IBedStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BedStatService> _logger;

        public BedStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BedStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<int>> GetAvailableBedsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.Beds.GetAvailableBedsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Available beds count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving available beds count.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<BedStatusDistributionDto>>> GetBedsDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Beds.GetBedsDistributionByStatusAsync();

                var distributionDtos = distribution.Select(d => new BedStatusDistributionDto
                {
                    Status = d.Key,
                    Count = d.Value
                }).ToList();

                return new ApiResponseDto<IEnumerable<BedStatusDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Beds status distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving beds status distribution.");
                throw;
            }
        }
    }
}