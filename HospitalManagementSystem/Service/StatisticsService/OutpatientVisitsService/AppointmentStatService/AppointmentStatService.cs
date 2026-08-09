using AutoMapper;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.AppointmentStatService
{
    public class AppointmentStatService : IAppointmentStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentStatService> _logger;

        public AppointmentStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AppointmentStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<AppointmentStatusDistributionDto>>> GetAppointmentsDistributionByStatusAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Appointments.GetAppointmentsDistributionByStatusAsync();

                var distributionDtos = distribution.Select(d => new AppointmentStatusDistributionDto
                {
                    Status = d.Status,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<AppointmentStatusDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Appointments distribution by status retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving appointments distribution by status.");
                throw;
            }
        }

        public async Task<ApiResponseDto<int>> GetTodayAppointmentsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.Appointments.GetTodayAppointmentsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Today's appointments count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving today's appointments count.");
                throw;
            }
        }
    }
}
