using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PharmacysService.SaleItemService
{
    public class SaleItemService : ISaleItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleItemService> _logger;

        public SaleItemService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SaleItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<SaleItemResponseDto>>> GetAllItemsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.SaleItems.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<SaleItemResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<SaleItemResponseDto>>
                {
                    Message = "Sale items retrieved successfully.",
                    Data = new PagedResultDto<SaleItemResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all sale items.");
                throw;
            }
        }

        public async Task<ApiResponseDto<SaleItemResponseDto>> GetItemByIdAsync(int id)
        {
            try
            {
                var item = await _unitOfWork.SaleItems.GetByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent SaleItem {ItemId}.", id);
                    throw new KeyNotFoundException("The sale item does not exist.");
                }

                return new ApiResponseDto<SaleItemResponseDto>
                {
                    Message = "Sale item retrieved successfully.",
                    Data = _mapper.Map<SaleItemResponseDto>(item)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving SaleItem {ItemId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<SaleItemResponseDto>>> GetItemsBySaleIdAsync(int saleId)
        {
            try
            {
                var items = await _unitOfWork.SaleItems.GetItemsBySaleIdAsync(saleId);

                return new ApiResponseDto<IEnumerable<SaleItemResponseDto>>
                {
                    Message = "Sale items retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<SaleItemResponseDto>>(items)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items for Sale {SaleId}.", saleId);
                throw;
            }
        }

        public async Task<ApiResponseDto<SaleItemResponseDto>> CreateItemAsync(CreateSaleItemDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var sale = await _unitOfWork.PharmacySales.GetByIdAsync(dto.SaleId);
                if (sale == null) throw new KeyNotFoundException("The specified sale does not exist.");

                var medicine = await _unitOfWork.Medicines.GetByIdAsync(dto.MedicineId);
                if (medicine == null) throw new KeyNotFoundException("The specified medicine does not exist.");

                var item = _mapper.Map<SaleItem>(dto);
                await _unitOfWork.SaleItems.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new SaleItem {ItemId}.", item.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<SaleItemResponseDto>
                {
                    Message = "Sale item created successfully.",
                    Data = _mapper.Map<SaleItemResponseDto>(item)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new sale item.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateItemAsync(UpdateSaleItemDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var item = await _unitOfWork.SaleItems.GetByIdAsync(dto.Id);
                if (item == null)
                {
                    _logger.LogWarning("Attempted to update non-existent SaleItem {ItemId}.", dto.Id);
                    throw new KeyNotFoundException("The sale item does not exist.");
                }

                _mapper.Map(dto, item);

                _unitOfWork.SaleItems.Update(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated SaleItem {ItemId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Sale item updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating SaleItem {ItemId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteItemAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var item = await _unitOfWork.SaleItems.GetByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent SaleItem {ItemId}.", id);
                    throw new KeyNotFoundException("The sale item does not exist.");
                }

                _unitOfWork.SaleItems.Delete(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted SaleItem {ItemId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Sale item deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting SaleItem {ItemId}.", id);
                throw;
            }
        }
    }
}