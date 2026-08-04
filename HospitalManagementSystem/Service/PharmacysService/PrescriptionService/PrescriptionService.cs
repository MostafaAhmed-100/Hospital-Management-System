using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PharmacysService.PrescriptionService
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionService> _logger;

        public PrescriptionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PrescriptionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>> GetAllPrescriptionsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Prescriptions.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PrescriptionResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>
                {
                    Message = "Prescriptions retrieved successfully.",
                    Data = new PagedResultDto<PrescriptionResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all prescriptions.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PrescriptionResponseDto>> GetPrescriptionByIdAsync(int id)
        {
            try
            {
                var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
                if (prescription == null)
                    throw new KeyNotFoundException("The prescription does not exist.");

                return new ApiResponseDto<PrescriptionResponseDto>
                {
                    Message = "Prescription retrieved successfully.",
                    Data = _mapper.Map<PrescriptionResponseDto>(prescription)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Prescription {PrescriptionId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PrescriptionWithItemsResponseDto>> GetPrescriptionWithItemsAsync(int id)
        {
            try
            {
                var prescription = await _unitOfWork.Prescriptions.GetPrescriptionWithItemsAsync(id);
                if (prescription == null)
                    throw new KeyNotFoundException("The prescription does not exist.");

                return new ApiResponseDto<PrescriptionWithItemsResponseDto>
                {
                    Message = "Prescription with items retrieved successfully.",
                    Data = _mapper.Map<PrescriptionWithItemsResponseDto>(prescription)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items for Prescription {PrescriptionId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<PrescriptionResponseDto>> CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.RecordId);
                if (record == null) throw new KeyNotFoundException("Medical record not found.");

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("Patient not found.");

                var prescription = _mapper.Map<Prescription>(dto);
                prescription.Status = PrescriptionStatus.Dispensed;

                await _unitOfWork.Prescriptions.AddAsync(prescription);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Prescription {PrescriptionId}.", prescription.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PrescriptionResponseDto>
                {
                    Message = "Prescription created successfully.",
                    Data = _mapper.Map<PrescriptionResponseDto>(prescription)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new prescription.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdatePrescriptionStatusAsync(UpdatePrescriptionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.Id);
                if (prescription == null)
                    throw new KeyNotFoundException("The prescription does not exist.");

                prescription.Status = dto.Status;
                _unitOfWork.Prescriptions.Update(prescription);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated status for Prescription {PrescriptionId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Prescription status updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Prescription {PrescriptionId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeletePrescriptionAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
                if (prescription == null)
                    throw new KeyNotFoundException("The prescription does not exist.");

                _unitOfWork.Prescriptions.Delete(prescription);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Prescription {PrescriptionId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Prescription deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Prescription {PrescriptionId}.", id);
                throw;
            }
        }
    }
}