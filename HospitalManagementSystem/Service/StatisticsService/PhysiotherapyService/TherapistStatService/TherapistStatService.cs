using AutoMapper;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.TherapistStatService
{
    public class TherapistStatService : ITherapistStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TherapistStatService> _logger;

        public TherapistStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TherapistStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopActiveTherapistDto>>> GetTopActiveTherapistsAsync()
        {
            try
            {
                var topTherapists = await _unitOfWork.Therapists.GetTopActiveTherapistsAsync();

                var therapistDtos = topTherapists.Select(d => new TopActiveTherapistDto
                {
                    TherapistName = d.TherapistName,
                    SessionsCount = d.SessionsCount
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopActiveTherapistDto>>
                {
                    IsSuccess = true,
                    Message = "Top active therapists retrieved successfully.",
                    StatusCode = 200,
                    Data = therapistDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top active therapists.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TherapistSpecializationDistributionDto>>> GetTherapistSpecializationDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Therapists.GetTherapistSpecializationDistributionAsync();

                var distributionDtos = distribution.Select(d => new TherapistSpecializationDistributionDto
                {
                    Specialization = d.Specialization,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TherapistSpecializationDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Therapist specialization distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving therapist specialization distribution.");
                throw;
            }
        }
    }
}
