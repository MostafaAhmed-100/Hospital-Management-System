using AutoMapper;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PhysiotherapyService.PhysioSessionService
{
    public class PhysioSessionService : IPhysioSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PhysioSessionService> _logger;

        public PhysioSessionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PhysioSessionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PhysioSessionResponseDto>>> GetAllSessionsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.PhysioSessions.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PhysioSessionResponseDto>>(items);

                var pagedResult = new PagedResultDto<PhysioSessionResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<PhysioSessionResponseDto>>
                {
                    Message = "Physiotherapy sessions retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all physio sessions.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PhysioSessionResponseDto>> GetSessionByIdAsync(int id)
        {
            try
            {
                var session = await _unitOfWork.PhysioSessions.GetByIdAsync(id);

                if (session == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent PhysioSession {SessionId}.", id);
                    throw new KeyNotFoundException("The physiotherapy session does not exist.");
                }

                return new ApiResponseDto<PhysioSessionResponseDto>
                {
                    Message = "Physiotherapy session retrieved successfully.",
                    Data = _mapper.Map<PhysioSessionResponseDto>(session)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving PhysioSession {SessionId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>> GetSessionsByPatientIdAsync(int patientId)
        {
            try
            {
                var sessions = await _unitOfWork.PhysioSessions.GetSessionsByPatientIdAsync(patientId);

                return new ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>
                {
                    Message = "Patient sessions retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<PhysioSessionResponseDto>>(sessions)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving sessions for Patient {PatientId}.", patientId);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>> GetSessionsByTherapistIdAsync(int therapistId)
        {
            try
            {
                var sessions = await _unitOfWork.PhysioSessions.GetSessionsByTherapistIdAsync(therapistId);

                return new ApiResponseDto<IEnumerable<PhysioSessionResponseDto>>
                {
                    Message = "Therapist sessions retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<PhysioSessionResponseDto>>(sessions)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving sessions for Therapist {TherapistId}.", therapistId);
                throw;
            }
        }

        public async Task<ApiResponseDto<PhysioSessionResponseDto>> CreateSessionAsync(CreatePhysioSessionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("The specified patient does not exist.");

                var therapist = await _unitOfWork.Therapists.GetByIdAsync(dto.TherapistId);
                if (therapist == null) throw new KeyNotFoundException("The specified therapist does not exist.");

                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.RecordId);
                if (record == null) throw new KeyNotFoundException("The specified medical record does not exist.");

                var session = _mapper.Map<PhysioSession>(dto);
                await _unitOfWork.PhysioSessions.AddAsync(session);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new PhysioSession {SessionId}.", session.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PhysioSessionResponseDto>
                {
                    Message = "Physiotherapy session created successfully.",
                    Data = _mapper.Map<PhysioSessionResponseDto>(session)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new physio session.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateSessionAsync(UpdatePhysioSessionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var session = await _unitOfWork.PhysioSessions.GetByIdAsync(dto.Id);
                if (session == null)
                {
                    _logger.LogWarning("Attempted to update non-existent PhysioSession {SessionId}.", dto.Id);
                    throw new KeyNotFoundException("The physiotherapy session does not exist.");
                }

                var therapist = await _unitOfWork.Therapists.GetByIdAsync(dto.TherapistId);
                if (therapist == null) throw new KeyNotFoundException("The specified therapist does not exist.");

                _mapper.Map(dto, session);

                _unitOfWork.PhysioSessions.Update(session);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated PhysioSession {SessionId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Physiotherapy session updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating PhysioSession {SessionId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteSessionAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var session = await _unitOfWork.PhysioSessions.GetByIdAsync(id);

                if (session == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent PhysioSession {SessionId}.", id);
                    throw new KeyNotFoundException("The physiotherapy session does not exist.");
                }

                session.IsDeleted = true;

                _unitOfWork.PhysioSessions.Update(session);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted PhysioSession {SessionId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Physiotherapy session deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting PhysioSession {SessionId}.", id);
                throw;
            }
        }
    }
}