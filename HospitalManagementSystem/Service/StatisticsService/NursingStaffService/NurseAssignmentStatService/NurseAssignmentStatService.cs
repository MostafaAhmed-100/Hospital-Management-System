using AutoMapper;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.NurseAssignmentStatService
{
    public class NurseAssignmentStatService : INurseAssignmentStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NurseAssignmentStatService> _logger;

        public NurseAssignmentStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<NurseAssignmentStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopAssignedNurseDto>>> GetTopAssignedNursesAsync()
        {
            try
            {
                var topNurses = await _unitOfWork.NurseAssignments.GetTopAssignedNursesAsync();

                var topNurseDtos = topNurses.Select(d => new TopAssignedNurseDto
                {
                    NurseLicenseNumber = d.LicenseNumber,
                    AssignmentCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopAssignedNurseDto>>
                {
                    IsSuccess = true,
                    Message = "Top assigned nurses retrieved successfully.",
                    StatusCode = 200,
                    Data = topNurseDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top assigned nurses.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<AssignmentShiftDistributionDto>>> GetAssignmentsDistributionByShiftAsync()
        {
            try
            {
                var distribution = await _unitOfWork.NurseAssignments.GetAssignmentsDistributionByShiftAsync();

                var distributionDtos = distribution.Select(d => new AssignmentShiftDistributionDto
                {
                    Shift = d.Shift,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<AssignmentShiftDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Assignments distribution by shift retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving assignments distribution by shift.");
                throw;
            }
        }
    }
}
