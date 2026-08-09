using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.ClinicDTOs;
using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.Clinics_DoctorsService
{
    public class ClinicsAndDoctorsStat : IClinicsAndDoctorsStat
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClinicsAndDoctorsStat> _logger;

        public ClinicsAndDoctorsStat(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ClinicsAndDoctorsStat> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponseDto<IEnumerable<ClinicResponseDto>>> GetTheMostClinicsWithAppointmentInDepartment(int departmentId)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(departmentId);
                if (department == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Department {DepartmentId}.", departmentId);
                    throw new KeyNotFoundException("The department does not exist.");
                }

                var clinics = await _unitOfWork.Clinics.GetTheMostClinicsWithAppointmentInDepartment(departmentId);
                var clinicDtos = _mapper.Map<IEnumerable<ClinicResponseDto>>(clinics);

                return new ApiResponseDto<IEnumerable<ClinicResponseDto>>
                {
                    IsSuccess = true,
                    Message = "Clinics retrieved successfully.",
                    StatusCode = 200,
                    Data = clinicDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while The Most Clinics With Appointments In Department.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ClinicResponseDto>>> GetTheMostClinicsWithAppointmentInHospital()
        {
            try
            {
                var clinics = await _unitOfWork.Clinics.GetTheMostClinicsWithAppointmentInHospital();
                var clinicDtos = _mapper.Map<IEnumerable<ClinicResponseDto>>(clinics);

                return new ApiResponseDto<IEnumerable<ClinicResponseDto>>
                {
                    IsSuccess = true,
                    Message = "Clinics retrieved successfully.",
                    StatusCode = 200,
                    Data = clinicDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving The Most Clinics With Appointment In Hospital.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTheMostDoctorsWithAppointmentsInDepartment(int departmentId)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(departmentId);
                if (department == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Department {DepartmentId}.", departmentId);
                    throw new KeyNotFoundException("The department does not exist.");
                }

                var doctors = await _unitOfWork.Doctors.GetTheMostDoctorsWithAppointmentsInDepartment(departmentId);
                var doctorsDtos = _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);

                return new ApiResponseDto<IEnumerable<DoctorResponseDto>>
                {
                    IsSuccess = true,
                    Message = "doctors retrieved successfully.",
                    StatusCode = 200,
                    Data = doctorsDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while The Most doctors With Appointments In Department.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTheMostDoctorsWithAppointmentsInHospital()
        {
            try
            {
                var doctors = await _unitOfWork.Doctors.GetTheMostDoctorsWithAppointmentsInHospital();
                var doctorDtos = _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);

                return new ApiResponseDto<IEnumerable<DoctorResponseDto>>
                {
                    IsSuccess = true,
                    Message = "Clinics retrieved successfully.",
                    StatusCode = 200,
                    Data = doctorDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving The Most doctors With Appointment In Hospital.");
                throw;
            }
        }
    }
}
