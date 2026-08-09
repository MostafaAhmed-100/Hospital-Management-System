using AutoMapper;
using HospitalManagementSystem.DTOs.LabTestDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.LabTestService
{
    public class LabTestStatService : ILabTestStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LabTestStatService> _logger;

        public LabTestStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LabTestStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<LabTestStatusDistributionDto>>> GetLabTestStatusDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.LabTests.GetLabTestStatusDistributionAsync();

                var distributionDtos = distribution.Select(d => new LabTestStatusDistributionDto
                {
                    Status = d.Status,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<LabTestStatusDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Lab test status distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving lab test status distribution.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TopLabTestDto>>> GetTopRequestedLabTestsAsync()
        {
            try
            {
                var topTests = await _unitOfWork.LabTests.GetTopRequestedLabTestsAsync();

                var testDtos = topTests.Select(d => new TopLabTestDto
                {
                    TestName = d.TestName,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopLabTestDto>>
                {
                    IsSuccess = true,
                    Message = "Top requested lab tests retrieved successfully.",
                    StatusCode = 200,
                    Data = testDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top requested lab tests.");
                throw;
            }
        }
    }
}