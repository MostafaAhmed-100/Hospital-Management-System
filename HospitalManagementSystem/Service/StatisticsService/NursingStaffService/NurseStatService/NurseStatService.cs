using AutoMapper;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseStatService
{
    public class NurseStatService : INurseStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NurseStatService> _logger;

        public NurseStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<NurseStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<NurseShiftDistributionDto>>> GetNursesDistributionByShiftAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Nurses.GetNursesDistributionByShiftAsync();

                var distributionDtos = distribution.Select(d => new NurseShiftDistributionDto
                {
                    Shift = d.Shift,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<NurseShiftDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Nurses distribution by shift retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving nurses distribution by shift.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<WardSpecializationCountDto>>> GetTopWardSpecializationsAsync()
        {
            try
            {
                var topWards = await _unitOfWork.Nurses.GetTopWardSpecializationsAsync();

                var wardDtos = topWards.Select(d => new WardSpecializationCountDto
                {
                    WardSpecialization = d.WardSpecialization,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<WardSpecializationCountDto>>
                {
                    IsSuccess = true,
                    Message = "Top ward specializations retrieved successfully.",
                    StatusCode = 200,
                    Data = wardDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top ward specializations.");
                throw;
            }
        }
    }
}
