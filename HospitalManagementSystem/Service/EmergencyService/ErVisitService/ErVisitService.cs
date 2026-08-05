using AutoMapper;
using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.EmergencyService.ErVisitService
{
    public class ErVisitService : IErVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ErVisitService> _logger;

        public ErVisitService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ErVisitService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<ErVisitDto>>> GetAllErVisitsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.ErVisits.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<ErVisitDto>>(items);

                return new ApiResponseDto<PagedResultDto<ErVisitDto>>
                {
                    Message = "ER visits retrieved successfully.",
                    Data = new PagedResultDto<ErVisitDto>
                    {
                        Items = mappedItems,
                        TotalCount = totalCount,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all ER visits.");
                throw;
            }
        }

        public async Task<ApiResponseDto<ErVisitDto>> GetErVisitByIdAsync(int id)
        {
            try
            {
                var visit = await _unitOfWork.ErVisits.GetByIdAsync(id);

                if (visit == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent ErVisit {VisitId}.", id);
                    throw new KeyNotFoundException("The ER visit does not exist.");
                }

                return new ApiResponseDto<ErVisitDto>
                {
                    Message = "ER visit retrieved successfully.",
                    Data = _mapper.Map<ErVisitDto>(visit)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ErVisit {VisitId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ErVisitDto>>> GetErQueueAsync()
        {
            try
            {
                var queue = await _unitOfWork.ErVisits.GetErQueueAsync();

                return new ApiResponseDto<IEnumerable<ErVisitDto>>
                {
                    Message = "ER queue retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<ErVisitDto>>(queue)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving the ER queue.");
                throw;
            }
        }

        public async Task<ApiResponseDto<ErVisitDto>> CreateErVisitAsync(CreateErVisitDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var visit = _mapper.Map<ErVisit>(dto);

                visit.ArrivalTime = DateTime.Now;
                visit.Status = ErVisitStatus.Pending;

                await _unitOfWork.ErVisits.AddAsync(visit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new ErVisit {VisitId}.", visit.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<ErVisitDto>
                {
                    Message = "ER visit created successfully.",
                    Data = _mapper.Map<ErVisitDto>(visit)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new ER visit.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateErVisitAsync(UpdateErVisitDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var visit = await _unitOfWork.ErVisits.GetByIdAsync(dto.Id);
                if (visit == null)
                {
                    _logger.LogWarning("Attempted to update non-existent ErVisit {VisitId}.", dto.Id);
                    throw new KeyNotFoundException("The ER visit does not exist.");
                }

                if ((visit.Status == ErVisitStatus.Admitted || visit.Status == ErVisitStatus.Discharged)
                    && (dto.Status == ErVisitStatus.Pending || dto.Status == ErVisitStatus.InTreatment))
                {
                    _logger.LogWarning("Invalid state transition attempted for ErVisit {VisitId}.", dto.Id);
                    throw new InvalidOperationException("لا يمكن إرجاع حالة المريض للانتظار بعد خروجه أو تنويمه.");
                }

                _mapper.Map(dto, visit);

                _unitOfWork.ErVisits.Update(visit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated ErVisit {VisitId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "ER visit updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating ErVisit {VisitId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteErVisitAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var visit = await _unitOfWork.ErVisits.GetByIdAsync(id);

                if (visit == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent ErVisit {VisitId}.", id);
                    throw new KeyNotFoundException("The ER visit does not exist.");
                }

                visit.IsDeleted = true;

                _unitOfWork.ErVisits.Update(visit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted ErVisit {VisitId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "ER visit deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting ErVisit {VisitId}.", id);
                throw;
            }
        }
    }
}