using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.OutpatientVisitsService.MedicalRecordService
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalRecordService> _logger;

        public MedicalRecordService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<MedicalRecordService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<MedicalRecordResponseDto>>> GetAllMedicalRecordsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.MedicalRecords.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<MedicalRecordResponseDto>>(items);

                var pagedResult = new PagedResultDto<MedicalRecordResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<MedicalRecordResponseDto>>
                {
                    Message = "Medical records retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all medical records.");
                throw;
            }
        }

        public async Task<ApiResponseDto<MedicalRecordResponseDto>> GetMedicalRecordByIdAsync(int id)
        {
            try
            {
                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(id);

                if (record == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Medical Record {RecordId}.", id);
                    throw new KeyNotFoundException("The medical record does not exist.");
                }

                return new ApiResponseDto<MedicalRecordResponseDto>
                {
                    Message = "Medical record retrieved successfully.",
                    Data = _mapper.Map<MedicalRecordResponseDto>(record)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Medical Record {RecordId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<MedicalRecordResponseDto>>> GetRecordsByPatientIdAsync(int patientId)
        {
            try
            {
                var records = await _unitOfWork.MedicalRecords.GetRecordsByPatientIdAsync(patientId);

                return new ApiResponseDto<IEnumerable<MedicalRecordResponseDto>>
                {
                    Message = "Patient medical records retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<MedicalRecordResponseDto>>(records)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving records for Patient {PatientId}.", patientId);
                throw;
            }
        }

        public async Task<ApiResponseDto<MedicalRecordResponseDto>> CreateMedicalRecordAsync(CreateMedicalRecordDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
                if (appointment == null) throw new KeyNotFoundException("The specified appointment does not exist.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("The specified patient does not exist.");

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null) throw new KeyNotFoundException("The specified doctor does not exist.");

                var medicalRecord = _mapper.Map<MedicalRecord>(dto);
                medicalRecord.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.MedicalRecords.AddAsync(medicalRecord);

                appointment.Status = AppointmentStatus.Completed;
                _unitOfWork.Appointments.Update(appointment);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Medical Record {RecordId}.", medicalRecord.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<MedicalRecordResponseDto>
                {
                    Message = "Medical record created successfully.",
                    Data = _mapper.Map<MedicalRecordResponseDto>(medicalRecord)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new medical record.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateMedicalRecordAsync(UpdateMedicalRecordDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.Id);
                if (record == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Medical Record {RecordId}.", dto.Id);
                    throw new KeyNotFoundException("The medical record does not exist.");
                }

                record.Diagnosis = dto.Diagnosis;

                _unitOfWork.MedicalRecords.Update(record);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Medical Record {RecordId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Medical record updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Medical Record {RecordId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteMedicalRecordAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(id);

                if (record == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Medical Record {RecordId}.", id);
                    throw new KeyNotFoundException("The medical record does not exist.");
                }

                record.IsDeleted = true;

                _unitOfWork.MedicalRecords.Update(record);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Medical Record {RecordId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Medical record deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Medical Record {RecordId}.", id);
                throw;
            }
        }
    }
}