using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.MedicineDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.MedicineService
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicineService> _logger;

        public MedicineService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<MedicineService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<MedicineResponseDto>>> GetAllMedicinesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Medicines.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<MedicineResponseDto>>(items);

                var pagedResult = new PagedResultDto<MedicineResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<MedicineResponseDto>>
                {
                    Message = "Medicines retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all medicines.");
                throw;
            }
        }

        public async Task<ApiResponseDto<MedicineResponseDto>> GetMedicineByIdAsync(int id)
        {
            try
            {
                var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);

                if (medicine == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Medicine {MedicineId}.", id);
                    throw new KeyNotFoundException("The medicine does not exist.");
                }

                return new ApiResponseDto<MedicineResponseDto>
                {
                    Message = "Medicine retrieved successfully.",
                    Data = _mapper.Map<MedicineResponseDto>(medicine)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Medicine {MedicineId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<MedicineResponseDto>>> SearchMedicinesByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ApiResponseDto<IEnumerable<MedicineResponseDto>>
                    {
                        Message = "Search parameter is empty.",
                        Data = new List<MedicineResponseDto>()
                    };
                }

                var medicines = await _unitOfWork.Medicines.SearchMedicinesByNameAsync(name);

                return new ApiResponseDto<IEnumerable<MedicineResponseDto>>
                {
                    Message = "Medicines searched successfully.",
                    Data = _mapper.Map<IEnumerable<MedicineResponseDto>>(medicines)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for medicine with name {MedicineName}.", name);
                throw;
            }
        }

        public async Task<ApiResponseDto<MedicineResponseDto>> CreateMedicineAsync(CreateMedicineDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var medicine = _mapper.Map<Medicine>(dto);
                await _unitOfWork.Medicines.AddAsync(medicine);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Medicine {MedicineId}.", medicine.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<MedicineResponseDto>
                {
                    Message = "Medicine created successfully.",
                    Data = _mapper.Map<MedicineResponseDto>(medicine)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new medicine.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateMedicineAsync(UpdateMedicineDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var medicine = await _unitOfWork.Medicines.GetByIdAsync(dto.Id);
                if (medicine == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Medicine {MedicineId}.", dto.Id);
                    throw new KeyNotFoundException("The medicine does not exist.");
                }

                _mapper.Map(dto, medicine);

                _unitOfWork.Medicines.Update(medicine);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Medicine {MedicineId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Medicine updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Medicine {MedicineId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteMedicineAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);

                if (medicine == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Medicine {MedicineId}.", id);
                    throw new KeyNotFoundException("The medicine does not exist.");
                }

                medicine.IsDeleted = true;

                _unitOfWork.Medicines.Update(medicine);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Medicine {MedicineId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Medicine deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Medicine {MedicineId}.", id);
                throw;
            }
        }
    }
}