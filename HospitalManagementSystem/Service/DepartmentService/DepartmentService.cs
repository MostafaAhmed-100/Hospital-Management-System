using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.DepartmentDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<DepartmentResponseDto>>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Departments.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<DepartmentResponseDto>>(items);

                var pagedResult = new PagedResultDto<DepartmentResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<DepartmentResponseDto>>
                {
                    Message = "Departments retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all departments.");
                throw;
            }
        }

        public async Task<ApiResponseDto<DepartmentWithDetailsResponseDto>> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetDepartmentWithClinicsAndDoctorsAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Department {DepartmentId}.", id);
                    throw new KeyNotFoundException("The department does not exist.");
                }

                return new ApiResponseDto<DepartmentWithDetailsResponseDto>
                {
                    Message = "Department retrieved successfully.",
                    Data = _mapper.Map<DepartmentWithDetailsResponseDto>(department)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Department {DepartmentId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<DepartmentResponseDto>> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = _mapper.Map<Department>(dto);
                await _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Department {DepartmentId}.", department.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<DepartmentResponseDto>
                {
                    Message = "Department created successfully.",
                    Data = _mapper.Map<DepartmentResponseDto>(department)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new department.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateDepartmentAsync(UpdateDepartmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(dto.Id);
                if (department == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Department {DepartmentId}.", dto.Id);
                    throw new KeyNotFoundException("The department does not exist.");
                }

                _mapper.Map(dto, department);

                _unitOfWork.Departments.Update(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Department {DepartmentId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Department updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Department {DepartmentId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteDepartmentAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetDepartmentWithClinicsAndDoctorsAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Department {DepartmentId}.", id);
                    throw new KeyNotFoundException("The department does not exist.");
                }

                if ((department.Clinics != null && department.Clinics.Any()) ||
                    (department.Doctors != null && department.Doctors.Any()))
                {
                    _logger.LogWarning("Security/Business Warning: Attempted to delete Department {DepartmentId} which contains active clinics or doctors.", id);
                    throw new InvalidOperationException("Cannot delete a department that contains active clinics or doctors.");
                }

                department.IsDeleted = true;
                _unitOfWork.Departments.Update(department);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Department {DepartmentId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Department deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Department {DepartmentId}.", id);
                throw;
            }
        }
    }
}