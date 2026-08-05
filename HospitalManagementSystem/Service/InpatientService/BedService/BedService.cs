using AutoMapper;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.InpatientService.BedService
{
    public class BedService : IBedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BedService> _logger;

        public BedService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BedService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<BedResponseDto>>> GetAllBedsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Beds.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<BedResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<BedResponseDto>>
                {
                    Message = "Beds retrieved successfully.",
                    Data = new PagedResultDto<BedResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all beds.");
                throw;
            }
        }

        public async Task<ApiResponseDto<BedResponseDto>> GetBedByIdAsync(int id)
        {
            try
            {
                var bed = await _unitOfWork.Beds.GetByIdAsync(id);
                if (bed == null)
                    throw new KeyNotFoundException("The bed does not exist.");

                return new ApiResponseDto<BedResponseDto>
                {
                    Message = "Bed retrieved successfully.",
                    Data = _mapper.Map<BedResponseDto>(bed)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Bed {BedId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<BedResponseDto>>> GetAvailableBedsAsync()
        {
            try
            {
                var availableBeds = await _unitOfWork.Beds.GetAvailableBedsAsync();

                return new ApiResponseDto<IEnumerable<BedResponseDto>>
                {
                    Message = "Available beds retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<BedResponseDto>>(availableBeds)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving available beds.");
                throw;
            }
        }

        public async Task<ApiResponseDto<BedResponseDto>> CreateBedAsync(CreateBedDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var room = await _unitOfWork.Rooms.GetByIdAsync(dto.RoomId);
                if (room == null)
                    throw new KeyNotFoundException("Room not found.");

                var bed = _mapper.Map<Bed>(dto);

                await _unitOfWork.Beds.AddAsync(bed);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Bed {BedId}.", bed.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<BedResponseDto>
                {
                    Message = "Bed created successfully.",
                    Data = _mapper.Map<BedResponseDto>(bed)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new bed.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateBedAsync(UpdateBedDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var bed = await _unitOfWork.Beds.GetByIdAsync(dto.Id);
                if (bed == null)
                    throw new KeyNotFoundException("The bed does not exist.");

                var room = await _unitOfWork.Rooms.GetByIdAsync(dto.RoomId);
                if (room == null)
                    throw new KeyNotFoundException("Room not found.");

                _mapper.Map(dto, bed);

                _unitOfWork.Beds.Update(bed);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Bed {BedId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Bed updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Bed {BedId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteBedAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var bed = await _unitOfWork.Beds.GetByIdAsync(id);
                if (bed == null)
                    throw new KeyNotFoundException("The bed does not exist.");

                _unitOfWork.Beds.Delete(bed);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Bed {BedId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Bed deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Bed {BedId}.", id);
                throw;
            }
        }
    }
}