using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacyDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.PharmacyService
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacyService> _logger;

        public PharmacyService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacyService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PharmacyResponseDto>>> GetAllPharmaciesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Pharmacies.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PharmacyResponseDto>>(items);

                var pagedResult = new PagedResultDto<PharmacyResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<PharmacyResponseDto>>
                {
                    Message = "Pharmacies retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all pharmacies.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyResponseDto>> GetPharmacyByIdAsync(int id)
        {
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetByIdAsync(id);

                if (pharmacy == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Pharmacy {PharmacyId}.", id);
                    throw new KeyNotFoundException("The pharmacy does not exist.");
                }

                return new ApiResponseDto<PharmacyResponseDto>
                {
                    Message = "Pharmacy retrieved successfully.",
                    Data = _mapper.Map<PharmacyResponseDto>(pharmacy)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Pharmacy {PharmacyId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyWithInventoryResponseDto>> GetPharmacyWithInventoryAsync(int id)
        {
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetPharmacyWithInventoryAsync(id);

                if (pharmacy == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Pharmacy {PharmacyId} with inventory.", id);
                    throw new KeyNotFoundException("The pharmacy does not exist.");
                }

                return new ApiResponseDto<PharmacyWithInventoryResponseDto>
                {
                    Message = "Pharmacy with inventory retrieved successfully.",
                    Data = _mapper.Map<PharmacyWithInventoryResponseDto>(pharmacy)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving inventory for Pharmacy {PharmacyId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyResponseDto>> CreatePharmacyAsync(CreatePharmacyDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pharmacy = _mapper.Map<Pharmacy>(dto);
                await _unitOfWork.Pharmacies.AddAsync(pharmacy);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Pharmacy {PharmacyId}.", pharmacy.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PharmacyResponseDto>
                {
                    Message = "Pharmacy created successfully.",
                    Data = _mapper.Map<PharmacyResponseDto>(pharmacy)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new pharmacy.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdatePharmacyAsync(UpdatePharmacyDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetByIdAsync(dto.Id);
                if (pharmacy == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Pharmacy {PharmacyId}.", dto.Id);
                    throw new KeyNotFoundException("The pharmacy does not exist.");
                }

                _mapper.Map(dto, pharmacy);

                _unitOfWork.Pharmacies.Update(pharmacy);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Pharmacy {PharmacyId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Pharmacy updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Pharmacy {PharmacyId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeletePharmacyAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetByIdAsync(id);

                if (pharmacy == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Pharmacy {PharmacyId}.", id);
                    throw new KeyNotFoundException("The pharmacy does not exist.");
                }

                pharmacy.IsDeleted = true;

                _unitOfWork.Pharmacies.Update(pharmacy);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Pharmacy {PharmacyId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Pharmacy deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Pharmacy {PharmacyId}.", id);
                throw;
            }
        }
    }
}