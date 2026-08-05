using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.InpatientService.AdmissionService
{
    public class AdmissionService : IAdmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AdmissionService> _logger;

        public AdmissionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AdmissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<AdmissionResponseDto>>> GetAllAdmissionsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Admissions.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<AdmissionResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<AdmissionResponseDto>>
                {
                    Message = "Admissions retrieved successfully.",
                    Data = new PagedResultDto<AdmissionResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all admissions.");
                throw;
            }
        }

        public async Task<ApiResponseDto<AdmissionResponseDto>> GetAdmissionByIdAsync(int id)
        {
            try
            {
                var admission = await _unitOfWork.Admissions.GetByIdAsync(id);
                if (admission == null)
                    throw new KeyNotFoundException("The admission record does not exist.");

                return new ApiResponseDto<AdmissionResponseDto>
                {
                    Message = "Admission retrieved successfully.",
                    Data = _mapper.Map<AdmissionResponseDto>(admission)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Admission {AdmissionId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<AdmissionResponseDto>>> GetActiveAdmissionsAsync()
        {
            try
            {
                var activeAdmissions = await _unitOfWork.Admissions.GetActiveAdmissionsAsync();

                return new ApiResponseDto<IEnumerable<AdmissionResponseDto>>
                {
                    Message = "Active admissions retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<AdmissionResponseDto>>(activeAdmissions)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving active admissions.");
                throw;
            }
        }

        public async Task<ApiResponseDto<AdmissionResponseDto>> CreateAdmissionAsync(CreateAdmissionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var bed = await _unitOfWork.Beds.GetByIdAsync(dto.BedId);
                if (bed == null) throw new KeyNotFoundException("Bed not found.");
                if (bed.Status != BedStatus.Available)
                    throw new InvalidOperationException("The selected bed is not available.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("Patient not found.");

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

                var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.RecordId);
                if (record == null) throw new KeyNotFoundException("Medical record not found.");

                var admission = _mapper.Map<Admission>(dto);
                admission.AdmissionDate = DateTime.Now;
                admission.Status = AdmissionStatus.Active; 

                await _unitOfWork.Admissions.AddAsync(admission);

                bed.Status = BedStatus.Occupied;
                _unitOfWork.Beds.Update(bed);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Admission {AdmissionId} for Patient {PatientId}.", admission.Id, dto.PatientId);
                await transaction.CommitAsync();

                return new ApiResponseDto<AdmissionResponseDto>
                {
                    Message = "Admission created successfully.",
                    Data = _mapper.Map<AdmissionResponseDto>(admission)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new admission.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateAdmissionAsync(UpdateAdmissionDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var admission = await _unitOfWork.Admissions.GetByIdAsync(dto.Id);
                if (admission == null)
                    throw new KeyNotFoundException("The admission record does not exist.");

                _mapper.Map(dto, admission);

                _unitOfWork.Admissions.Update(admission);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Admission {AdmissionId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Admission updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Admission {AdmissionId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DischargePatientAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var admission = await _unitOfWork.Admissions.GetByIdAsync(id);
                if (admission == null)
                    throw new KeyNotFoundException("The admission record does not exist.");

                if (admission.Status == AdmissionStatus.Discharged)
                    throw new InvalidOperationException("Patient is already discharged.");

                admission.Status = AdmissionStatus.Discharged;
                admission.DischargeDate = DateTime.Now;
                _unitOfWork.Admissions.Update(admission);

                var bed = await _unitOfWork.Beds.GetByIdAsync(admission.BedId);
                if (bed != null)
                {
                    bed.Status = BedStatus.Available;
                    _unitOfWork.Beds.Update(bed);
                }

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully discharged Patient from Admission {AdmissionId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Patient discharged successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while discharging from Admission {AdmissionId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteAdmissionAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var admission = await _unitOfWork.Admissions.GetByIdAsync(id);
                if (admission == null)
                    throw new KeyNotFoundException("The admission record does not exist.");

                _unitOfWork.Admissions.Delete(admission);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Admission {AdmissionId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Admission deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Admission {AdmissionId}.", id);
                throw;
            }
        }
    }
}
