using AutoMapper;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.SurgeryService.SurgeryTeamStatService
{
    public class SurgeryTeamStatService : ISurgeryTeamStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SurgeryTeamStatService> _logger;

        public SurgeryTeamStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SurgeryTeamStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopActiveSurgeryStaffDto>>> GetTopActiveSurgeryStaffAsync()
        {
            try
            {
                var topStaff = await _unitOfWork.SurgeryTeams.GetTopActiveSurgeryStaffAsync();

                var staffDtos = topStaff.Select(d => new TopActiveSurgeryStaffDto
                {
                    StaffName = d.StaffName,
                    ParticipationsCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopActiveSurgeryStaffDto>>
                {
                    IsSuccess = true,
                    Message = "Top active surgery staff retrieved successfully.",
                    StatusCode = 200,
                    Data = staffDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top active surgery staff.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<SurgeryRoleDistributionDto>>> GetSurgeryRoleDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.SurgeryTeams.GetSurgeryRoleDistributionAsync();

                var distributionDtos = distribution.Select(d => new SurgeryRoleDistributionDto
                {
                    Role = d.Role,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<SurgeryRoleDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Surgery role distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving surgery role distribution.");
                throw;
            }
        }
    }
}
