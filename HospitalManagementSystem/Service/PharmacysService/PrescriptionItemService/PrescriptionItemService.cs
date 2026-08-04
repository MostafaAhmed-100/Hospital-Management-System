using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionItemDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PharmacysService.PrescriptionItemService
{
    public class PrescriptionItemService : IPrescriptionItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionItemService> _logger;

        public PrescriptionItemService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PrescriptionItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PrescriptionItemResponseDto>>> GetAllItemsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.PrescriptionItems.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PrescriptionItemResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<PrescriptionItemResponseDto>>
                {
                    Message = "Prescription items retrieved successfully.",
                    Data = new PagedResultDto<PrescriptionItemResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all prescription items.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PrescriptionItemResponseDto>> GetItemByIdAsync(int id)
        {
            try
            {
                var item = await _unitOfWork.PrescriptionItems.GetByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent PrescriptionItem {ItemId}.", id);
                    throw new KeyNotFoundException("The prescription item does not exist.");
                }

                return new ApiResponseDto<PrescriptionItemResponseDto>
                {
                    Message = "Prescription item retrieved successfully.",
                    Data = _mapper.Map<PrescriptionItemResponseDto>(item)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving PrescriptionItem {ItemId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PrescriptionItemResponseDto>>> GetItemsByPrescriptionIdAsync(int prescriptionId)
        {
            try
            {
                var items = await _unitOfWork.PrescriptionItems.GetItemsByPrescriptionIdAsync(prescriptionId);

                return new ApiResponseDto<IEnumerable<PrescriptionItemResponseDto>>
                {
                    Message = "Prescription items retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<PrescriptionItemResponseDto>>(items)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items for Prescription {PrescriptionId}.", prescriptionId);
                throw;
            }
        }

        public async Task<ApiResponseDto<PrescriptionItemResponseDto>> CreateItemAsync(CreatePrescriptionItemDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
                if (prescription == null) throw new KeyNotFoundException("The specified prescription does not exist.");

                var medicine = await _unitOfWork.Medicines.GetByIdAsync(dto.MedicineId);
                if (medicine == null) throw new KeyNotFoundException("The specified medicine does not exist.");

                var item = _mapper.Map<PrescriptionItem>(dto);
                await _unitOfWork.PrescriptionItems.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new PrescriptionItem {ItemId}.", item.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PrescriptionItemResponseDto>
                {
                    Message = "Prescription item created successfully.",
                    Data = _mapper.Map<PrescriptionItemResponseDto>(item)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new prescription item.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateItemAsync(UpdatePrescriptionItemDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var item = await _unitOfWork.PrescriptionItems.GetByIdAsync(dto.Id);
                if (item == null)
                {
                    _logger.LogWarning("Attempted to update non-existent PrescriptionItem {ItemId}.", dto.Id);
                    throw new KeyNotFoundException("The prescription item does not exist.");
                }

                _mapper.Map(dto, item);

                _unitOfWork.PrescriptionItems.Update(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated PrescriptionItem {ItemId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Prescription item updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating PrescriptionItem {ItemId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteItemAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var item = await _unitOfWork.PrescriptionItems.GetByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent PrescriptionItem {ItemId}.", id);
                    throw new KeyNotFoundException("The prescription item does not exist.");
                }

                _unitOfWork.PrescriptionItems.Delete(item);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted PrescriptionItem {ItemId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Prescription item deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting PrescriptionItem {ItemId}.", id);
                throw;
            }
        }
    }
}