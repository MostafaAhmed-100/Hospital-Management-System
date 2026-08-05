using AutoMapper;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.NursingStaffService.NurseAssignmentService
{
    public class NurseAssignmentService : INurseAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NurseAssignmentService> _logger;

        public NurseAssignmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<NurseAssignmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<NurseAssignmentResponseDto>>> GetAllAssignmentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.NurseAssignments.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<NurseAssignmentResponseDto>>(items);

                var pagedResult = new PagedResultDto<NurseAssignmentResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<NurseAssignmentResponseDto>>
                {
                    Message = "Nurse assignments retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all nurse assignments.");
                throw;
            }
        }

        public async Task<ApiResponseDto<NurseAssignmentResponseDto>> GetAssignmentByIdAsync(int id)
        {
            try
            {
                var assignment = await _unitOfWork.NurseAssignments.GetByIdAsync(id);

                if (assignment == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Nurse Assignment {AssignmentId}.", id);
                    throw new KeyNotFoundException("The nurse assignment does not exist.");
                }

                return new ApiResponseDto<NurseAssignmentResponseDto>
                {
                    Message = "Nurse assignment retrieved successfully.",
                    Data = _mapper.Map<NurseAssignmentResponseDto>(assignment)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Nurse Assignment {AssignmentId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<NurseAssignmentResponseDto>>> GetAssignmentsByNurseIdAsync(int nurseId)
        {
            try
            {
                var assignments = await _unitOfWork.NurseAssignments.GetAssignmentsByNurseIdAsync(nurseId);

                return new ApiResponseDto<IEnumerable<NurseAssignmentResponseDto>>
                {
                    Message = "Nurse assignments retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<NurseAssignmentResponseDto>>(assignments)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving assignments for Nurse {NurseId}.", nurseId);
                throw;
            }
        }

        public async Task<ApiResponseDto<NurseAssignmentResponseDto>> CreateAssignmentAsync(CreateNurseAssignmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var nurse = await _unitOfWork.Nurses.GetByIdAsync(dto.NurseId);
                if (nurse == null)
                    throw new KeyNotFoundException("الممرض غير موجود.");

                if (dto.AdmissionId.HasValue)
                {
                    var admission = await _unitOfWork.Admissions.GetByIdAsync(dto.AdmissionId.Value);
                    if (admission == null) throw new KeyNotFoundException("سجل التنويم غير موجود.");
                }

                if (dto.ErVisitId.HasValue)
                {
                    var erVisit = await _unitOfWork.ErVisits.GetByIdAsync(dto.ErVisitId.Value);
                    if (erVisit == null) throw new KeyNotFoundException("زيارة الطوارئ غير موجودة.");
                }

                var assignment = _mapper.Map<NurseAssignment>(dto);
                assignment.AssignedAt = DateTime.Now;

                await _unitOfWork.NurseAssignments.AddAsync(assignment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Nurse Assignment {AssignmentId}.", assignment.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<NurseAssignmentResponseDto>
                {
                    Message = "Nurse assignment created successfully.",
                    Data = _mapper.Map<NurseAssignmentResponseDto>(assignment)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new nurse assignment.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateAssignmentAsync(UpdateNurseAssignmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var assignment = await _unitOfWork.NurseAssignments.GetByIdAsync(dto.Id);
                if (assignment == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Nurse Assignment {AssignmentId}.", dto.Id);
                    throw new KeyNotFoundException("The nurse assignment does not exist.");
                }

                var nurse = await _unitOfWork.Nurses.GetByIdAsync(dto.NurseId);
                if (nurse == null) throw new KeyNotFoundException("الممرض غير موجود.");

                if (dto.AdmissionId.HasValue)
                {
                    var admission = await _unitOfWork.Admissions.GetByIdAsync(dto.AdmissionId.Value);
                    if (admission == null) throw new KeyNotFoundException("سجل التنويم غير موجود.");
                }

                if (dto.ErVisitId.HasValue)
                {
                    var erVisit = await _unitOfWork.ErVisits.GetByIdAsync(dto.ErVisitId.Value);
                    if (erVisit == null) throw new KeyNotFoundException("زيارة الطوارئ غير موجودة.");
                }

                _mapper.Map(dto, assignment);

                _unitOfWork.NurseAssignments.Update(assignment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Nurse Assignment {AssignmentId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Nurse assignment updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Nurse Assignment {AssignmentId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteAssignmentAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var assignment = await _unitOfWork.NurseAssignments.GetByIdAsync(id);

                if (assignment == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Nurse Assignment {AssignmentId}.", id);
                    throw new KeyNotFoundException("The nurse assignment does not exist.");
                }

                assignment.IsDeleted = true;

                _unitOfWork.NurseAssignments.Update(assignment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Nurse Assignment {AssignmentId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Nurse assignment deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Nurse Assignment {AssignmentId}.", id);
                throw;
            }
        }
    }
}