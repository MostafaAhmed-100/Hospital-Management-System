using AutoMapper;
using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.RoomStatService
{
    public class RoomStatService : IRoomStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomStatService> _logger;

        public RoomStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RoomStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<RoomTypeDistributionDto>>> GetRoomsDistributionByTypeAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Rooms.GetRoomsDistributionByTypeAsync();

                var distributionDtos = distribution.Select(d => new RoomTypeDistributionDto
                {
                    RoomType = d.RoomType,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<RoomTypeDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Rooms distribution by type retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving rooms distribution by type.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<DepartmentRoomCountDto>>> GetTopDepartmentsByRoomCountAsync()
        {
            try
            {
                var topDepartments = await _unitOfWork.Rooms.GetTopDepartmentsByRoomCountAsync();

                var departmentDtos = topDepartments.Select(d => new DepartmentRoomCountDto
                {
                    DepartmentName = d.DepartmentName,
                    RoomsCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<DepartmentRoomCountDto>>
                {
                    IsSuccess = true,
                    Message = "Top departments by room count retrieved successfully.",
                    StatusCode = 200,
                    Data = departmentDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top departments by room count.");
                throw;
            }
        }
    }
}