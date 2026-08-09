using AutoMapper;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PhysiotherapyService.PhysioSessionStatService
{
    public class PhysioSessionStatService : IPhysioSessionStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PhysioSessionStatService> _logger;

        public PhysioSessionStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PhysioSessionStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponseDto<IEnumerable<TopTherapyTypeDto>>> GetTopTherapyTypesAsync()
        {
            try
            {
                var topTherapies = await _unitOfWork.PhysioSessions.GetTopTherapyTypesAsync();

                var therapyDtos = topTherapies.Select(d => new TopTherapyTypeDto
                {
                    TherapyType = d.TherapyType,
                    SessionsCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopTherapyTypeDto>>
                {
                    IsSuccess = true,
                    Message = "Top therapy types retrieved successfully.",
                    StatusCode = 200,
                    Data = therapyDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top therapy types.");
                throw;
            }
        }
        public async Task<ApiResponseDto<int>> GetTodayPhysioSessionsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.PhysioSessions.GetTodayPhysioSessionsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Today's physio sessions count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving today's physio sessions count.");
                throw;
            }
        }
    }
}
