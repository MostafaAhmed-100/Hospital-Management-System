using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.SurgeryService.SurgeryRecordService
{
    public class SurgeryRecordService : ISurgeryRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SurgeryRecordService> _logger;

        public SurgeryRecordService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SurgeryRecordService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<SurgeryRecordResponseDto>>> GetAllSurgeryRecordsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.SurgeryRecords.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<SurgeryRecordResponseDto>>(items);

                var pagedResult = new PagedResultDto<SurgeryRecordResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<SurgeryRecordResponseDto>>
                {
                    Message = "Surgery records retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all surgery records.");
                throw;
            }
        }

        public async Task<ApiResponseDto<SurgeryRecordResponseDto>> GetSurgeryRecordByIdAsync(int id)
        {
            try
            {
                var surgery = await _unitOfWork.SurgeryRecords.GetByIdAsync(id);

                if (surgery == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Surgery Record {SurgeryId}.", id);
                    throw new KeyNotFoundException("The surgery record does not exist.");
                }

                return new ApiResponseDto<SurgeryRecordResponseDto>
                {
                    Message = "Surgery record retrieved successfully.",
                    Data = _mapper.Map<SurgeryRecordResponseDto>(surgery)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Surgery Record {SurgeryId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<SurgeryRecordResponseDto>> CreateSurgeryRecordAsync(CreateSurgeryRecordDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                bool isOverlapping = await _unitOfWork.SurgeryRecords
                    .HasOverlappingSurgeryAsync(dto.OperatingRoomId, dto.StartTime, dto.EndTime);

                if (isOverlapping)
                {
                    throw new InvalidOperationException("هناك عملية أخرى محجوزة في هذه الغرفة خلال نفس الوقت المحدد.");
                }

                var or = await _unitOfWork.OperatingRooms.GetByIdAsync(dto.OperatingRoomId);
                if (or == null) throw new KeyNotFoundException("غرفة العمليات غير موجودة.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("المريض غير موجود.");

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.LeadSurgeonId);
                if (doctor == null) throw new KeyNotFoundException("الجراح الأساسي غير موجود.");

                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.RecordId);
                if (record == null) throw new KeyNotFoundException("السجل الطبي غير موجود.");

                var surgery = _mapper.Map<SurgeryRecord>(dto);
                await _unitOfWork.SurgeryRecords.AddAsync(surgery);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Surgery Record {SurgeryId}.", surgery.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<SurgeryRecordResponseDto>
                {
                    Message = "Surgery record created successfully.",
                    Data = _mapper.Map<SurgeryRecordResponseDto>(surgery)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new surgery record.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateSurgeryRecordAsync(UpdateSurgeryRecordDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var surgery = await _unitOfWork.SurgeryRecords.GetByIdAsync(dto.Id);
                if (surgery == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Surgery Record {SurgeryId}.", dto.Id);
                    throw new KeyNotFoundException("The surgery record does not exist.");
                }

                bool isOverlapping = await _unitOfWork.SurgeryRecords
                    .HasOverlappingSurgeryAsync(dto.OperatingRoomId, dto.StartTime, dto.EndTime, dto.Id);

                if (isOverlapping)
                {
                    throw new InvalidOperationException("لا يمكن تعديل الموعد: يوجد تداخل مع عملية أخرى في نفس الغرفة.");
                }

                _mapper.Map(dto, surgery);

                _unitOfWork.SurgeryRecords.Update(surgery);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Surgery Record {SurgeryId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Surgery record updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Surgery Record {SurgeryId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteSurgeryRecordAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var surgery = await _unitOfWork.SurgeryRecords.GetByIdAsync(id);

                if (surgery == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Surgery Record {SurgeryId}.", id);
                    throw new KeyNotFoundException("The surgery record does not exist.");
                }

                surgery.IsDeleted = true;

                _unitOfWork.SurgeryRecords.Update(surgery);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Surgery Record {SurgeryId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Surgery record deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Surgery Record {SurgeryId}.", id);
                throw;
            }
        }
    }
}