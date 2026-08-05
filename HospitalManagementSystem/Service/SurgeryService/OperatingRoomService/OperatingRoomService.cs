using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.SurgeryService.OperatingRoomService
{
    public class OperatingRoomService : IOperatingRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OperatingRoomService> _logger;

        public OperatingRoomService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<OperatingRoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<OperatingRoomResponseDto>>> GetAllOperatingRoomsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.OperatingRooms.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<OperatingRoomResponseDto>>(items);

                var pagedResult = new PagedResultDto<OperatingRoomResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<OperatingRoomResponseDto>>
                {
                    Message = "Operating rooms retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all operating rooms.");
                throw;
            }
        }

        public async Task<ApiResponseDto<OperatingRoomResponseDto>> GetOperatingRoomByIdAsync(int id)
        {
            try
            {
                var room = await _unitOfWork.OperatingRooms.GetByIdAsync(id);

                if (room == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Operating Room {RoomId}.", id);
                    throw new KeyNotFoundException("The operating room does not exist.");
                }

                return new ApiResponseDto<OperatingRoomResponseDto>
                {
                    Message = "Operating room retrieved successfully.",
                    Data = _mapper.Map<OperatingRoomResponseDto>(room)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Operating Room {RoomId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<OperatingRoomResponseDto>>> GetAvailableOperatingRoomsAsync()
        {
            try
            {
                var availableRooms = await _unitOfWork.OperatingRooms.GetAvailableOperatingRoomsAsync();

                return new ApiResponseDto<IEnumerable<OperatingRoomResponseDto>>
                {
                    Message = "Available operating rooms retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<OperatingRoomResponseDto>>(availableRooms)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving available operating rooms.");
                throw;
            }
        }

        public async Task<ApiResponseDto<OperatingRoomResponseDto>> CreateOperatingRoomAsync(CreateOperatingRoomDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = _mapper.Map<OperatingRoom>(dto);
                await _unitOfWork.OperatingRooms.AddAsync(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Operating Room {RoomId}.", room.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<OperatingRoomResponseDto>
                {
                    Message = "Operating room created successfully.",
                    Data = _mapper.Map<OperatingRoomResponseDto>(room)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new operating room.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateOperatingRoomAsync(UpdateOperatingRoomDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = await _unitOfWork.OperatingRooms.GetByIdAsync(dto.Id);
                if (room == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Operating Room {RoomId}.", dto.Id);
                    throw new KeyNotFoundException("The operating room does not exist.");
                }

                _mapper.Map(dto, room);

                _unitOfWork.OperatingRooms.Update(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Operating Room {RoomId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Operating room updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Operating Room {RoomId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteOperatingRoomAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = await _unitOfWork.OperatingRooms.GetByIdAsync(id);

                if (room == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Operating Room {RoomId}.", id);
                    throw new KeyNotFoundException("The operating room does not exist.");
                }

                room.IsDeleted = true;

                _unitOfWork.OperatingRooms.Update(room);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Operating Room {RoomId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Operating room deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Operating Room {RoomId}.", id);
                throw;
            }
        }
    }
}