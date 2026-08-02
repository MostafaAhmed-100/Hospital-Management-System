using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.ClinicDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.ClinicService
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClinicService> _logger;

        public ClinicService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ClinicService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<ClinicResponseDto>>> GetAllClinicsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Clinics.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<ClinicResponseDto>>(items);

                var pagedResult = new PagedResultDto<ClinicResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<ClinicResponseDto>>
                {
                    Message = "Clinics retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all clinics.");
                throw;
            }
        }

        public async Task<ApiResponseDto<ClinicResponseDto>> GetClinicByIdAsync(int id)
        {
            try
            {
                var clinic = await _unitOfWork.Clinics.GetByIdAsync(id);

                if (clinic == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Clinic {ClinicId}.", id);
                    throw new KeyNotFoundException("The clinic does not exist.");
                }

                return new ApiResponseDto<ClinicResponseDto>
                {
                    Message = "Clinic retrieved successfully.",
                    Data = _mapper.Map<ClinicResponseDto>(clinic)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Clinic {ClinicId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<ClinicResponseDto>> CreateClinicAsync(CreateClinicDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var departmentExists = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (departmentExists == null)
                {
                    _logger.LogWarning("Attempted to create a clinic for a non-existent Department {DepartmentId}.", dto.DepartmentId);
                    throw new KeyNotFoundException("The specified department does not exist.");
                }

                var clinic = _mapper.Map<Clinic>(dto);
                await _unitOfWork.Clinics.AddAsync(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Clinic {ClinicId}.", clinic.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<ClinicResponseDto>
                {
                    Message = "Clinic created successfully.",
                    Data = _mapper.Map<ClinicResponseDto>(clinic)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new clinic.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateClinicAsync(UpdateClinicDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var clinic = await _unitOfWork.Clinics.GetByIdAsync(dto.Id);
                if (clinic == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Clinic {ClinicId}.", dto.Id);
                    throw new KeyNotFoundException("The clinic does not exist.");
                }

                if (clinic.DepartmentId != dto.DepartmentId)
                {
                    var departmentExists = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                    if (departmentExists == null)
                    {
                        throw new KeyNotFoundException("The specified new department does not exist.");
                    }
                }

                _mapper.Map(dto, clinic);

                _unitOfWork.Clinics.Update(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Clinic {ClinicId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Clinic updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Clinic {ClinicId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteClinicAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var clinic = await _unitOfWork.Clinics.GetByIdAsync(id);

                if (clinic == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Clinic {ClinicId}.", id);
                    throw new KeyNotFoundException("The clinic does not exist.");
                }

                clinic.IsDeleted = true;

                _unitOfWork.Clinics.Update(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Clinic {ClinicId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Clinic deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Clinic {ClinicId}.", id);
                throw;
            }
        }
    }
}