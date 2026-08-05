using AutoMapper;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.InpatientService.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;

        public RoomService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<RoomResponseDto>>> GetAllRoomsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Rooms.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<RoomResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<RoomResponseDto>>
                {
                    Message = "Rooms retrieved successfully.",
                    Data = new PagedResultDto<RoomResponseDto>
                    {
                        Items = mappedItems,
                        TotalCount = totalCount,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all rooms.");
                throw;
            }
        }

        public async Task<ApiResponseDto<RoomResponseDto>> GetRoomByIdAsync(int id)
        {
            try
            {
                var room = await _unitOfWork.Rooms.GetByIdAsync(id);
                if (room == null)
                    throw new KeyNotFoundException("The room does not exist.");

                return new ApiResponseDto<RoomResponseDto>
                {
                    Message = "Room retrieved successfully.",
                    Data = _mapper.Map<RoomResponseDto>(room)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Room {RoomId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<RoomResponseDto>>> GetRoomsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                var rooms = await _unitOfWork.Rooms.GetRoomsByDepartmentIdAsync(departmentId);

                return new ApiResponseDto<IEnumerable<RoomResponseDto>>
                {
                    Message = "Rooms retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<RoomResponseDto>>(rooms)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving rooms for Department {DepartmentId}.", departmentId);
                throw;
            }
        }

        public async Task<ApiResponseDto<RoomResponseDto>> CreateRoomAsync(CreateRoomDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (department == null)
                    throw new KeyNotFoundException("Department not found.");

                var room = _mapper.Map<Room>(dto);

                await _unitOfWork.Rooms.AddAsync(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Room {RoomId}.", room.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<RoomResponseDto>
                {
                    Message = "Room created successfully.",
                    Data = _mapper.Map<RoomResponseDto>(room)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new room.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateRoomAsync(UpdateRoomDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = await _unitOfWork.Rooms.GetByIdAsync(dto.Id);
                if (room == null)
                    throw new KeyNotFoundException("The room does not exist.");

                var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (department == null)
                    throw new KeyNotFoundException("Department not found.");

                _mapper.Map(dto, room);

                _unitOfWork.Rooms.Update(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Room {RoomId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Room updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Room {RoomId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteRoomAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = await _unitOfWork.Rooms.GetByIdAsync(id);
                if (room == null)
                    throw new KeyNotFoundException("The room does not exist.");

                _unitOfWork.Rooms.Delete(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Room {RoomId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Room deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Room {RoomId}.", id);
                throw;
            }
        }
    }
}