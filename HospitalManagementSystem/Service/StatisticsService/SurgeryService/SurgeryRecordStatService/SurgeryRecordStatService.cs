using AutoMapper;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryRecordStatService
{
    public class SurgeryRecordStatService : ISurgeryRecordStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SurgeryRecordStatService> _logger;

        public SurgeryRecordStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SurgeryRecordStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopSurgeryTypeDto>>> GetTopSurgeryTypesAsync()
        {
            try
            {
                var topSurgeries = await _unitOfWork.SurgeryRecords.GetTopSurgeryTypesAsync();

                var surgeryDtos = topSurgeries.Select(d => new TopSurgeryTypeDto
                {
                    SurgeryType = d.SurgeryType,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopSurgeryTypeDto>>
                {
                    IsSuccess = true,
                    Message = "Top surgery types retrieved successfully.",
                    StatusCode = 200,
                    Data = surgeryDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top surgery types.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<SurgeryStatusDistributionDto>>> GetSurgeryStatusDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.SurgeryRecords.GetSurgeryStatusDistributionAsync();

                var distributionDtos = distribution.Select(d => new SurgeryStatusDistributionDto
                {
                    Status = d.Status,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<SurgeryStatusDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Surgery status distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving surgery status distribution.");
                throw;
            }
        }
    }
}
