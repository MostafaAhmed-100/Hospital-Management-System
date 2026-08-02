using AutoMapper;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.PatientDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PatientService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PatientResponseDto>>> GetAllPatientsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Patients.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PatientResponseDto>>(items);

                var pagedResult = new PagedResultDto<PatientResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<PatientResponseDto>>
                {
                    Message = "Patients retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all patients.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PatientResponseDto>> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await _unitOfWork.Patients.GetByIdAsync(id);

                if (patient == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Patient {PatientId}.", id);
                    throw new KeyNotFoundException("The patient does not exist.");
                }

                return new ApiResponseDto<PatientResponseDto>
                {
                    Message = "Patient retrieved successfully.",
                    Data = _mapper.Map<PatientResponseDto>(patient)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Patient {PatientId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PatientWithMedicalHistoryResponseDto>> GetPatientWithMedicalHistoryAsync(int id)
        {
            try
            {
                var patient = await _unitOfWork.Patients.GetPatientWithMedicalHistoryAsync(id);

                if (patient == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Patient {PatientId} with medical history.", id);
                    throw new KeyNotFoundException("The patient does not exist.");
                }

                return new ApiResponseDto<PatientWithMedicalHistoryResponseDto>
                {
                    Message = "Patient with medical history retrieved successfully.",
                    Data = _mapper.Map<PatientWithMedicalHistoryResponseDto>(patient)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving medical history for Patient {PatientId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PatientResponseDto>> CreatePatientAsync(CreatePatientDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (dto.InsuranceId.HasValue)
                {
                    var insurance = await _unitOfWork.InsuranceProviders.GetByIdAsync(dto.InsuranceId.Value);
                    if (insurance == null) 
                        throw new KeyNotFoundException("The specified insurance provider does not exist.");
                }

                var patient = _mapper.Map<Patient>(dto);
                await _unitOfWork.Patients.AddAsync(patient);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Patient {PatientId}.", patient.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PatientResponseDto>
                {
                    Message = "Patient created successfully.",
                    Data = _mapper.Map<PatientResponseDto>(patient)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new patient.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdatePatientAsync(UpdatePatientDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.Id);
                if (patient == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Patient {PatientId}.", dto.Id);
                    throw new KeyNotFoundException("The patient does not exist.");
                }

                _mapper.Map(dto, patient);

                _unitOfWork.Patients.Update(patient);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Patient {PatientId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Patient updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Patient {PatientId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeletePatientAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var patient = await _unitOfWork.Patients.GetByIdAsync(id);

                if (patient == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Patient {PatientId}.", id);
                    throw new KeyNotFoundException("The patient does not exist.");
                }
                patient.IsDeleted = true;

                _unitOfWork.Patients.Update(patient);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Patient {PatientId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Patient deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Patient {PatientId}.", id);
                throw;
            }
        }
    }
}