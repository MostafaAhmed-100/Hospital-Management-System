using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SpecialtyDTOs;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.SpecialtyService
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SpecialtyService> _logger;

        public SpecialtyService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SpecialtyService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<SpecialtyResponseDto>>> GetAllSpecialtiesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Specialties.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<SpecialtyResponseDto>>(items);

                var pagedResult = new PagedResultDto<SpecialtyResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<SpecialtyResponseDto>>
                {
                    Message = "Specialties retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all specialties.");
                throw;
            }
        }

        public async Task<ApiResponseDto<SpecialtyWithDoctorsResponseDto>> GetSpecialtyByIdAsync(int id)
        {
            try
            {
                var specialty = await _unitOfWork.Specialties.GetSpecialtyWithDoctorsAsync(id);

                if (specialty == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Specialty {SpecialtyId}.", id);
                    throw new KeyNotFoundException("The specialty does not exist.");
                }

                return new ApiResponseDto<SpecialtyWithDoctorsResponseDto>
                {
                    Message = "Specialty retrieved successfully.",
                    Data = _mapper.Map<SpecialtyWithDoctorsResponseDto>(specialty)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Specialty {SpecialtyId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<SpecialtyResponseDto>> CreateSpecialtyAsync(CreateSpecialtyDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var specialty = _mapper.Map<Specialty>(dto);
                await _unitOfWork.Specialties.AddAsync(specialty);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Specialty {SpecialtyId}.", specialty.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<SpecialtyResponseDto>
                {
                    Message = "Specialty created successfully.",
                    Data = _mapper.Map<SpecialtyResponseDto>(specialty)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new specialty.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateSpecialtyAsync(UpdateSpecialtyDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var specialty = await _unitOfWork.Specialties.GetByIdAsync(dto.Id);
                if (specialty == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Specialty {SpecialtyId}.", dto.Id);
                    throw new KeyNotFoundException("The specialty does not exist.");
                }

                _mapper.Map(dto, specialty);

                _unitOfWork.Specialties.Update(specialty);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Specialty {SpecialtyId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Specialty updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Specialty {SpecialtyId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteSpecialtyAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var specialty = await _unitOfWork.Specialties.GetSpecialtyWithDoctorsAsync(id);

                if (specialty == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Specialty {SpecialtyId}.", id);
                    throw new KeyNotFoundException("The specialty does not exist.");
                }

                if (specialty.Doctors != null && specialty.Doctors.Any())
                {
                    _logger.LogWarning("Security/Business Warning: Attempted to delete Specialty {SpecialtyId} which contains active doctors.", id);
                    throw new InvalidOperationException("Cannot delete a specialty that contains active doctors.");
                }

                specialty.IsDeleted = true;

                _unitOfWork.Specialties.Update(specialty);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Specialty {SpecialtyId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Specialty deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Specialty {SpecialtyId}.", id);
                throw;
            }
        }
    }
}  