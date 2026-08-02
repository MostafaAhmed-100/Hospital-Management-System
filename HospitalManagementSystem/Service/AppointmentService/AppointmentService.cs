using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.AppointmentDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<AppointmentResponseDto>>> GetAllAppointmentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Appointments.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<AppointmentResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<AppointmentResponseDto>>
                {
                    Message = "Appointments retrieved successfully.",
                    Data = new PagedResultDto<AppointmentResponseDto>
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
                _logger.LogError(ex, "Error occurred while retrieving all appointments.");
                throw;
            }
        }

        public async Task<ApiResponseDto<AppointmentResponseDto>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
                if (appointment == null)
                    throw new KeyNotFoundException("The appointment does not exist.");

                return new ApiResponseDto<AppointmentResponseDto>
                {
                    Message = "Appointment retrieved successfully.",
                    Data = _mapper.Map<AppointmentResponseDto>(appointment)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Appointment {AppointmentId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<AppointmentResponseDto>>> GetUpcomingAppointmentsByDoctorAsync(int doctorId)
        {
            try
            {
                var appointments = await _unitOfWork.Appointments.GetUpcomingAppointmentsByDoctorAsync(doctorId);
                var mappedAppointments = _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);

                return new ApiResponseDto<IEnumerable<AppointmentResponseDto>>
                {
                    Message = "Upcoming appointments retrieved successfully.",
                    Data = mappedAppointments
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving upcoming appointments for Doctor {DoctorId}.", doctorId);
                throw;
            }
        }

        public async Task<ApiResponseDto<AppointmentResponseDto>> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("Patient not found.");

                var clinic = await _unitOfWork.Clinics.GetByIdAsync(dto.ClinicId);
                if (clinic == null) throw new KeyNotFoundException("Clinic not found.");

                bool hasConflict = await _unitOfWork.Appointments.HasConflictAsync(dto.DoctorId, dto.AppointmentDate);
                if (hasConflict)
                    throw new InvalidOperationException("The doctor already has an appointment at the requested time.");

                var appointment = _mapper.Map<Appointment>(dto);

                appointment.Status = AppointmentStatus.Pending;

                await _unitOfWork.Appointments.AddAsync(appointment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Appointment {AppointmentId}.", appointment.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<AppointmentResponseDto>
                {
                    Message = "Appointment created successfully.",
                    Data = _mapper.Map<AppointmentResponseDto>(appointment)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new appointment.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateAppointmentAsync(UpdateAppointmentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.Id);
                if (appointment == null)
                    throw new KeyNotFoundException("The appointment does not exist.");

                if (appointment.AppointmentDate != dto.AppointmentDate || appointment.DoctorId != dto.DoctorId)
                {
                    bool hasConflict = await _unitOfWork.Appointments.HasConflictAsync(dto.DoctorId, dto.AppointmentDate);
                    if (hasConflict)
                        throw new InvalidOperationException("The doctor already has an appointment at the newly requested time.");
                }

                _mapper.Map(dto, appointment);
                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Appointment {AppointmentId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Appointment updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Appointment {AppointmentId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteAppointmentAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
                if (appointment == null)
                    throw new KeyNotFoundException("The appointment does not exist.");

                appointment.IsDeleted = true;

                _unitOfWork.Appointments.Update(appointment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Appointment {AppointmentId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Appointment deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Appointment {AppointmentId}.", id);
                throw;
            }
        }
    }
}