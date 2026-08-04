using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.DTOs.InsuranceProviderDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.InsuranceProviderService
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<InsuranceProviderService> _logger;

        public InsuranceProviderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<InsuranceProviderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponseDto<InsuranceProviderWithPatientsResponseDto>> GetProviderWithPatientsAsync(int id)
        {
            try
            {
                var provider = await _unitOfWork.InsuranceProviders.GetProviderWithPatientsAsync(id);

                if (provider == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent InsuranceProvider {ProviderId} with patients.", id);
                    throw new KeyNotFoundException("The insurance provider does not exist.");
                }

                return new ApiResponseDto<InsuranceProviderWithPatientsResponseDto>
                {
                    Message = "Insurance provider with patients retrieved successfully.",
                    Data = _mapper.Map<InsuranceProviderWithPatientsResponseDto>(provider)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving patients for InsuranceProvider {ProviderId}.", id);
                throw;
            }
        }
        public async Task<ApiResponseDto<PagedResultDto<InsuranceProviderResponseDto>>> GetAllProvidersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.InsuranceProviders.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<InsuranceProviderResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<InsuranceProviderResponseDto>>
                {
                    Message = "Insurance providers retrieved successfully.",
                    Data = new PagedResultDto<InsuranceProviderResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all insurance providers.");
                throw;
            }
        }

        public async Task<ApiResponseDto<InsuranceProviderResponseDto>> GetProviderByIdAsync(int id)
        {
            try
            {
                var provider = await _unitOfWork.InsuranceProviders.GetByIdAsync(id);

                if (provider == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent InsuranceProvider {ProviderId}.", id);
                    throw new KeyNotFoundException("The insurance provider does not exist.");
                }

                return new ApiResponseDto<InsuranceProviderResponseDto>
                {
                    Message = "Insurance provider retrieved successfully.",
                    Data = _mapper.Map<InsuranceProviderResponseDto>(provider)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving InsuranceProvider {ProviderId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<InsuranceProviderResponseDto>> CreateProviderAsync(CreateInsuranceProviderDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var provider = _mapper.Map<InsuranceProvider>(dto);
                await _unitOfWork.InsuranceProviders.AddAsync(provider);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new InsuranceProvider {ProviderId}.", provider.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<InsuranceProviderResponseDto>
                {
                    Message = "Insurance provider created successfully.",
                    Data = _mapper.Map<InsuranceProviderResponseDto>(provider)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new insurance provider.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateProviderAsync(UpdateInsuranceProviderDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var provider = await _unitOfWork.InsuranceProviders.GetByIdAsync(dto.Id);
                if (provider == null)
                {
                    _logger.LogWarning("Attempted to update non-existent InsuranceProvider {ProviderId}.", dto.Id);
                    throw new KeyNotFoundException("The insurance provider does not exist.");
                }

                _mapper.Map(dto, provider);

                _unitOfWork.InsuranceProviders.Update(provider);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated InsuranceProvider {ProviderId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Insurance provider updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating InsuranceProvider {ProviderId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteProviderAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var provider = await _unitOfWork.InsuranceProviders.GetByIdAsync(id);

                if (provider == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent InsuranceProvider {ProviderId}.", id);
                    throw new KeyNotFoundException("The insurance provider does not exist.");
                }

                provider.IsDeleted = true;

                _unitOfWork.InsuranceProviders.Update(provider);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted InsuranceProvider {ProviderId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Insurance provider deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting InsuranceProvider {ProviderId}.", id);
                throw;
            }
        }
    }
}