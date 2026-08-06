using AutoMapper;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.PhysiotherapyService.TherapistService
{
    public class TherapistService : ITherapistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TherapistService> _logger;

        public TherapistService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TherapistService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<TherapistResponseDto>>> GetAllTherapistsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Therapists.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<TherapistResponseDto>>(items);

                var pagedResult = new PagedResultDto<TherapistResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<TherapistResponseDto>>
                {
                    Message = "Therapists retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all therapists.");
                throw;
            }
        }

        public async Task<ApiResponseDto<TherapistResponseDto>> GetTherapistByIdAsync(int id)
        {
            try
            {
                var therapist = await _unitOfWork.Therapists.GetByIdAsync(id);

                if (therapist == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Therapist {TherapistId}.", id);
                    throw new KeyNotFoundException("The therapist does not exist.");
                }

                return new ApiResponseDto<TherapistResponseDto>
                {
                    Message = "Therapist retrieved successfully.",
                    Data = _mapper.Map<TherapistResponseDto>(therapist)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Therapist {TherapistId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TherapistResponseDto>>> GetTherapistsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                var therapists = await _unitOfWork.Therapists.GetTherapistsByDepartmentIdAsync(departmentId);

                return new ApiResponseDto<IEnumerable<TherapistResponseDto>>
                {
                    Message = "Therapists retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<TherapistResponseDto>>(therapists)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving therapists for Department {DepartmentId}.", departmentId);
                throw;
            }
        }

        public async Task<ApiResponseDto<TherapistResponseDto>> CreateTherapistAsync(CreateTherapistDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (department == null)
                    throw new KeyNotFoundException("The specified department does not exist.");

                var therapist = _mapper.Map<Therapist>(dto);
                await _unitOfWork.Therapists.AddAsync(therapist);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Therapist {TherapistId}.", therapist.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<TherapistResponseDto>
                {
                    Message = "Therapist created successfully.",
                    Data = _mapper.Map<TherapistResponseDto>(therapist)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new therapist.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateTherapistAsync(UpdateTherapistDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var therapist = await _unitOfWork.Therapists.GetByIdAsync(dto.Id);
                if (therapist == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Therapist {TherapistId}.", dto.Id);
                    throw new KeyNotFoundException("The therapist does not exist.");
                }

                var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (department == null)
                    throw new KeyNotFoundException("The specified department does not exist.");

                _mapper.Map(dto, therapist);

                _unitOfWork.Therapists.Update(therapist);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Therapist {TherapistId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Therapist updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Therapist {TherapistId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteTherapistAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var therapist = await _unitOfWork.Therapists.GetByIdAsync(id);

                if (therapist == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Therapist {TherapistId}.", id);
                    throw new KeyNotFoundException("The therapist does not exist.");
                }

                therapist.IsDeleted = true;

                _unitOfWork.Therapists.Update(therapist);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Therapist {TherapistId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Therapist deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Therapist {TherapistId}.", id);
                throw;
            }
        }
    }
}