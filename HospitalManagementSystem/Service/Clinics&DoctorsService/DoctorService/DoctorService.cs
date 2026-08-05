using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DoctorService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<DoctorResponseDto>>> GetAllDoctorsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Doctors.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<DoctorResponseDto>>(items);

                var pagedResult = new PagedResultDto<DoctorResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<DoctorResponseDto>>
                {
                    Message = "Doctors retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all doctors.");
                throw;
            }
        }

        public async Task<ApiResponseDto<DoctorResponseDto>> GetDoctorByIdAsync(int id)
        {
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

                if (doctor == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Doctor {DoctorId}.", id);
                    throw new KeyNotFoundException("The doctor does not exist.");
                }

                return new ApiResponseDto<DoctorResponseDto>
                {
                    Message = "Doctor retrieved successfully.",
                    Data = _mapper.Map<DoctorResponseDto>(doctor)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Doctor {DoctorId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<DoctorResponseDto>> CreateDoctorAsync(CreateDoctorDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                if (department == null) throw new KeyNotFoundException("The specified department does not exist.");

                var specialty = await _unitOfWork.Specialties.GetByIdAsync(dto.SpecialtyId);
                if (specialty == null) throw new KeyNotFoundException("The specified specialty does not exist.");
                if (!string.IsNullOrEmpty(dto.FullName))
                    throw new KeyNotFoundException ("the doctor name Does not exist");
                var doctor = _mapper.Map<Doctor>(dto);
                await _unitOfWork.Doctors.AddAsync(doctor);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Doctor {DoctorId}.", doctor.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<DoctorResponseDto>
                {
                    Message = "Doctor created successfully.",
                    Data = _mapper.Map<DoctorResponseDto>(doctor)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new doctor.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateDoctorAsync(UpdateDoctorDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.Id);
                if (doctor == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Doctor {DoctorId}.", dto.Id);
                    throw new KeyNotFoundException("The doctor does not exist.");
                }

                if (doctor.DepartmentId != dto.DepartmentId)
                {
                    var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
                    if (department == null) throw new KeyNotFoundException("The specified new department does not exist.");
                }

                if (doctor.SpecialtyId != dto.SpecialtyId)
                {
                    var specialty = await _unitOfWork.Specialties.GetByIdAsync(dto.SpecialtyId);
                    if (specialty == null) throw new KeyNotFoundException("The specified new specialty does not exist.");
                }

                _mapper.Map(dto, doctor);

                _unitOfWork.Doctors.Update(doctor);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Doctor {DoctorId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Doctor updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Doctor {DoctorId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteDoctorAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

                if (doctor == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Doctor {DoctorId}.", id);
                    throw new KeyNotFoundException("The doctor does not exist.");
                }

                doctor.IsDeleted = true;

                _unitOfWork.Doctors.Update(doctor);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Doctor {DoctorId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Doctor deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Doctor {DoctorId}.", id);
                throw;
            }
        }
    }
}