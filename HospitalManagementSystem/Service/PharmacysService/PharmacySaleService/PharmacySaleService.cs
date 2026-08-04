using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PharmacysService.PharmacySaleService
{
    public class PharmacySaleService : IPharmacySaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PharmacySaleService> _logger;

        public PharmacySaleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PharmacySaleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PharmacySaleResponseDto>>> GetAllSalesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.PharmacySales.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PharmacySaleResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<PharmacySaleResponseDto>>
                {
                    Message = "Sales retrieved successfully.",
                    Data = new PagedResultDto<PharmacySaleResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all sales.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacySaleResponseDto>> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _unitOfWork.PharmacySales.GetByIdAsync(id);

                if (sale == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Sale {SaleId}.", id);
                    throw new KeyNotFoundException("The sale record does not exist.");
                }

                return new ApiResponseDto<PharmacySaleResponseDto>
                {
                    Message = "Sale retrieved successfully.",
                    Data = _mapper.Map<PharmacySaleResponseDto>(sale)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Sale {SaleId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacySaleWithItemsResponseDto>> GetSaleWithItemsAsync(int id)
        {
            try
            {
                var sale = await _unitOfWork.PharmacySales.GetSaleWithItemsAsync(id);

                if (sale == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Sale {SaleId} with items.", id);
                    throw new KeyNotFoundException("The sale record does not exist.");
                }

                return new ApiResponseDto<PharmacySaleWithItemsResponseDto>
                {
                    Message = "Sale with items retrieved successfully.",
                    Data = _mapper.Map<PharmacySaleWithItemsResponseDto>(sale)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items for Sale {SaleId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PharmacySaleResponseDto>> CreateSaleAsync(CreatePharmacySaleDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pharmacy = await _unitOfWork.Pharmacies.GetByIdAsync(dto.PharmacyId);
                if (pharmacy == null) throw new KeyNotFoundException("The specified pharmacy does not exist.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("The specified patient does not exist.");

                if (dto.PrescriptionId.HasValue)
                {

                    var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId.Value);
                    if (prescription == null) throw new KeyNotFoundException("The specified prescription does not exist.");
                }

                var sale = _mapper.Map<PharmacySale>(dto);
                await _unitOfWork.PharmacySales.AddAsync(sale);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Pharmacy Sale {SaleId}.", sale.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PharmacySaleResponseDto>
                {
                    Message = "Sale created successfully.",
                    Data = _mapper.Map<PharmacySaleResponseDto>(sale)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new sale.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateSaleAsync(UpdatePharmacySaleDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var sale = await _unitOfWork.PharmacySales.GetByIdAsync(dto.Id);
                if (sale == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Sale {SaleId}.", dto.Id);
                    throw new KeyNotFoundException("The sale record does not exist.");
                }

                _mapper.Map(dto, sale);

                _unitOfWork.PharmacySales.Update(sale);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Pharmacy Sale {SaleId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Sale updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Sale {SaleId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteSaleAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var sale = await _unitOfWork.PharmacySales.GetByIdAsync(id);

                if (sale == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Sale {SaleId}.", id);
                    throw new KeyNotFoundException("The sale record does not exist.");
                }

                _unitOfWork.PharmacySales.Delete(sale);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Pharmacy Sale {SaleId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Sale deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Sale {SaleId}.", id);
                throw;
            }
        }
    }
}