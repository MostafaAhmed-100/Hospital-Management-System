using AutoMapper;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.PatientDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.PatientStatService
{
    public class PatientStatService : IPatientStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientStatService> _logger;

        public PatientStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PatientStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<PatientInsuranceDistributionDto>>> GetPatientInsuranceDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Patients.GetPatientInsuranceDistributionAsync();

                var distributionDtos = distribution.Select(d => new PatientInsuranceDistributionDto
                {
                    Category = d.Category,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<PatientInsuranceDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Patient insurance distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving patient insurance distribution.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TopPatientDto>>> GetTopFrequentPatientsAsync()
        {
            try
            {
                var topPatients = await _unitOfWork.Patients.GetTopFrequentPatientsAsync();

                var patientDtos = topPatients.Select(d => new TopPatientDto
                {
                    PatientName = d.PatientName,
                    AppointmentsCount = d.AppointmentsCount
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopPatientDto>>
                {
                    IsSuccess = true,
                    Message = "Top frequent patients retrieved successfully.",
                    StatusCode = 200,
                    Data = patientDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top frequent patients.");
                throw;
            }
        }
    }
}
