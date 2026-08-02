using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacyInventoryDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.PharmacyInventoryService
{
    public class PharmacyInventoryService : IPharmacyInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacyInventoryService> _logger;

        public PharmacyInventoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacyInventoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PharmacyInventoryResponseDto>>> GetAllInventoryAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.PharmacyInventories.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PharmacyInventoryResponseDto>>(items);

                var pagedResult = new PagedResultDto<PharmacyInventoryResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<PharmacyInventoryResponseDto>>
                {
                    Message = "Inventory retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all inventory.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyInventoryResponseDto>> GetInventoryByIdAsync(int id)
        {
            try
            {
                var inventory = await _unitOfWork.PharmacyInventories.GetByIdAsync(id);

                if (inventory == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Inventory {InventoryId}.", id);
                    throw new KeyNotFoundException("The inventory record does not exist.");
                }

                return new ApiResponseDto<PharmacyInventoryResponseDto>
                {
                    Message = "Inventory retrieved successfully.",
                    Data = _mapper.Map<PharmacyInventoryResponseDto>(inventory)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Inventory {InventoryId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyInventoryResponseDto>> CheckMedicineStockAsync(int pharmacyId, int medicineId)
        {
            try
            {
                var inventory = await _unitOfWork.PharmacyInventories.CheckMedicineStockAsync(pharmacyId, medicineId);

                if (inventory == null)
                {
                    return new ApiResponseDto<PharmacyInventoryResponseDto>
                    {
                        Message = "Medicine is not available in this pharmacy.",
                        Data = null
                    };
                }

                return new ApiResponseDto<PharmacyInventoryResponseDto>
                {
                    Message = "Stock checked successfully.",
                    Data = _mapper.Map<PharmacyInventoryResponseDto>(inventory)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking stock for Medicine {MedicineId} in Pharmacy {PharmacyId}.", medicineId, pharmacyId);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacyInventoryResponseDto>> CreateOrUpdateInventoryAsync(CreatePharmacyInventoryDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetByIdAsync(dto.PharmacyId);
                if (pharmacy == null) throw new KeyNotFoundException("The specified pharmacy does not exist.");

                var medicine = await _unitOfWork.Medicines.GetByIdAsync(dto.MedicineId);
                if (medicine == null) throw new KeyNotFoundException("The specified medicine does not exist.");

                var existingInventory = await _unitOfWork.PharmacyInventories.CheckMedicineStockAsync(dto.PharmacyId, dto.MedicineId);

                PharmacyInventory inventoryEntity;

                if (existingInventory != null)
                {
                    existingInventory.Quantity += dto.Quantity;
                    existingInventory.ExpiryDate = dto.ExpiryDate; 

                    _unitOfWork.PharmacyInventories.Update(existingInventory);
                    inventoryEntity = existingInventory;
                    _logger.LogInformation("Updated existing inventory for Medicine {MedicineId} in Pharmacy {PharmacyId}.", dto.MedicineId, dto.PharmacyId);
                }
                else
                {
                    inventoryEntity = _mapper.Map<PharmacyInventory>(dto);
                    await _unitOfWork.PharmacyInventories.AddAsync(inventoryEntity);
                    _logger.LogInformation("Added new inventory record for Medicine {MedicineId} in Pharmacy {PharmacyId}.", dto.MedicineId, dto.PharmacyId);
                }

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ApiResponseDto<PharmacyInventoryResponseDto>
                {
                    Message = "Inventory processed successfully.",
                    Data = _mapper.Map<PharmacyInventoryResponseDto>(inventoryEntity)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while processing inventory.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateInventoryAsync(UpdatePharmacyInventoryDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var inventory = await _unitOfWork.PharmacyInventories.GetByIdAsync(dto.Id);
                if (inventory == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Inventory {InventoryId}.", dto.Id);
                    throw new KeyNotFoundException("The inventory record does not exist.");
                }

                _mapper.Map(dto, inventory);

                _unitOfWork.PharmacyInventories.Update(inventory);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Inventory {InventoryId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Inventory updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Inventory {InventoryId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteInventoryAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var inventory = await _unitOfWork.PharmacyInventories.GetByIdAsync(id);

                if (inventory == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Inventory {InventoryId}.", id);
                    throw new KeyNotFoundException("The inventory record does not exist.");
                }

                _unitOfWork.PharmacyInventories.Delete(inventory);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully hard-deleted Inventory {InventoryId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Inventory deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Inventory {InventoryId}.", id);
                throw;
            }
        }
    }
}