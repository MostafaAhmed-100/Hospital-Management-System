using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.DTOs.LabTestDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.LabTestService
{
    public class LabTestService : ILabTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LabTestService> _logger;

        public LabTestService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LabTestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<LabTestResponseDto>>> GetAllLabTestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.LabTests.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<LabTestResponseDto>>(items);

                var pagedResult = new PagedResultDto<LabTestResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<LabTestResponseDto>>
                {
                    Message = "Lab tests retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all lab tests.");
                throw;
            }
        }

        public async Task<ApiResponseDto<LabTestResponseDto>> GetLabTestByIdAsync(int id)
        {
            try
            {
                var labTest = await _unitOfWork.LabTests.GetByIdAsync(id);

                if (labTest == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent LabTest {TestId}.", id);
                    throw new KeyNotFoundException("The lab test does not exist.");
                }

                return new ApiResponseDto<LabTestResponseDto>
                {
                    Message = "Lab test retrieved successfully.",
                    Data = _mapper.Map<LabTestResponseDto>(labTest)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving LabTest {TestId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<LabTestResponseDto>>> GetTestsByRecordIdAsync(int recordId)
        {
            try
            {
                var tests = await _unitOfWork.LabTests.GetTestsByRecordIdAsync(recordId);

                return new ApiResponseDto<IEnumerable<LabTestResponseDto>>
                {
                    Message = "Record tests retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<LabTestResponseDto>>(tests)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving tests for Record {RecordId}.", recordId);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<LabTestResponseDto>>> GetPendingTestsAsync()
        {
            try
            {
                var pendingTests = await _unitOfWork.LabTests.GetPendingTestsAsync();

                return new ApiResponseDto<IEnumerable<LabTestResponseDto>>
                {
                    Message = "Pending lab tests retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<LabTestResponseDto>>(pendingTests)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving pending lab tests.");
                throw;
            }
        }

        public async Task<ApiResponseDto<LabTestResponseDto>> CreateLabTestAsync(CreateLabTestDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.RecordId);
                if (record == null)
                    throw new KeyNotFoundException("السجل الطبي غير موجود.");

                var labTest = _mapper.Map<LabTest>(dto);
                labTest.Status = LabTestStatus.Pending;

                await _unitOfWork.LabTests.AddAsync(labTest);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new LabTest {TestId}.", labTest.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<LabTestResponseDto>
                {
                    Message = "Lab test created successfully.",
                    Data = _mapper.Map<LabTestResponseDto>(labTest)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new lab test.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateLabTestResultAsync(UpdateLabTestResultDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var labTest = await _unitOfWork.LabTests.GetByIdAsync(dto.Id);
                if (labTest == null)
                {
                    _logger.LogWarning("Attempted to update non-existent LabTest {TestId}.", dto.Id);
                    throw new KeyNotFoundException("التحليل غير موجود.");
                }

                _mapper.Map(dto, labTest);

                _unitOfWork.LabTests.Update(labTest);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated LabTest {TestId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Lab test result updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating LabTest {TestId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteLabTestAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var labTest = await _unitOfWork.LabTests.GetByIdAsync(id);

                if (labTest == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent LabTest {TestId}.", id);
                    throw new KeyNotFoundException("The lab test does not exist.");
                }

                labTest.IsDeleted = true;

                _unitOfWork.LabTests.Update(labTest);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted LabTest {TestId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Lab test deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting LabTest {TestId}.", id);
                throw;
            }
        }
    }
}