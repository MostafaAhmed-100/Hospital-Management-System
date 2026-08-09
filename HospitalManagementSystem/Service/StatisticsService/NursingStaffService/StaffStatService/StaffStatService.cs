using AutoMapper;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.NursingStaffService.StaffStatService
{
    public class StaffStatService : IStaffStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffStatService> _logger;

        public StaffStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<StaffStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<StaffRoleDistributionDto>>> GetStaffDistributionByRoleAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Staff.GetStaffDistributionByRoleAsync();

                var distributionDtos = distribution.Select(d => new StaffRoleDistributionDto
                {
                    Role = d.Role,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<StaffRoleDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Staff distribution by role retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving staff distribution by role.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ClinicStaffCountDto>>> GetTopClinicsByStaffCountAsync()
        {
            try
            {
                var topClinics = await _unitOfWork.Staff.GetTopClinicsByStaffCountAsync();

                var clinicDtos = topClinics.Select(d => new ClinicStaffCountDto
                {
                    ClinicName = d.ClinicName,
                    StaffCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<ClinicStaffCountDto>>
                {
                    IsSuccess = true,
                    Message = "Top clinics by staff count retrieved successfully.",
                    StatusCode = 200,
                    Data = clinicDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top clinics by staff count.");
                throw;
            }
        }
    }
}