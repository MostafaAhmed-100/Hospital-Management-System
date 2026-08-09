using AutoMapper;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.OutpatientVisitsService.MedicalRecordStatService
{
    public class MedicalRecordStatService : IMedicalRecordStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalRecordStatService> _logger;

        public MedicalRecordStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<MedicalRecordStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<TopDiagnosisDto>>> GetTopDiagnosesAsync()
        {
            try
            {
                var topDiagnoses = await _unitOfWork.MedicalRecords.GetTopDiagnosesAsync();

                var diagnosesDtos = topDiagnoses.Select(d => new TopDiagnosisDto
                {
                    Diagnosis = d.Diagnosis,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopDiagnosisDto>>
                {
                    IsSuccess = true,
                    Message = "Top diagnoses retrieved successfully.",
                    StatusCode = 200,
                    Data = diagnosesDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top diagnoses.");
                throw;
            }
        }

        public async Task<ApiResponseDto<int>> GetTodayMedicalRecordsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.MedicalRecords.GetTodayMedicalRecordsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Today's medical records count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving today's medical records count.");
                throw;
            }
        }
    }
}
