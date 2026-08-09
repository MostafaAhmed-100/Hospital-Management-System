using AutoMapper;
using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.EmergencyService
{
    public class ErVisitStatService : IErVisitStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ErVisitStatService> _logger;

        public ErVisitStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ErVisitStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTopDoctorsInErAsync()
        {
            try
            {
                var doctors = await _unitOfWork.ErVisits.GetTopDoctorsInErAsync();
                var doctorDtos = _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);

                return new ApiResponseDto<IEnumerable<DoctorResponseDto>>
                {
                    IsSuccess = true,
                    Message = "Top ER doctors retrieved successfully.",
                    StatusCode = 200,
                    Data = doctorDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top doctors in ER.");
                throw;
            }
        }

        public async Task<ApiResponseDto<int>> GetActiveErVisitsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.ErVisits.GetActiveErVisitsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Active ER visits count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving active ER visits count.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TriageDistributionDto>>> GetErVisitsDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.ErVisits.GetErVisitsDistributionByTriageLevelAsync();

                var distributionDtos = distribution.Select(d => new TriageDistributionDto
                {
                    TriageLevel = d.Key,
                    Count = d.Value
                }).ToList();

                return new ApiResponseDto<IEnumerable<TriageDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Triage distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ER triage distribution.");
                throw;
            }
        }
    }
}
