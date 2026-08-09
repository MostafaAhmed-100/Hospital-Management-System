using AutoMapper;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.PharmacysService.PrescriptionStatService
{
    public class PrescriptionStatService : IPrescriptionStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionStatService> _logger;

        public PrescriptionStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PrescriptionStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<IEnumerable<PrescriptionStatusDistributionDto>>> GetPrescriptionStatusDistributionAsync()
        {
            try
            {
                var distribution = await _unitOfWork.Prescriptions.GetPrescriptionStatusDistributionAsync();

                var distributionDtos = distribution.Select(d => new PrescriptionStatusDistributionDto
                {
                    Status = d.Status,
                    Count = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<PrescriptionStatusDistributionDto>>
                {
                    IsSuccess = true,
                    Message = "Prescription status distribution retrieved successfully.",
                    StatusCode = 200,
                    Data = distributionDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving prescription status distribution.");
                throw;
            }
        }
        public async Task<ApiResponseDto<IEnumerable<TopPrescribingDoctorDto>>> GetTopPrescribingDoctorsAsync()
        {
            try
            {
                var topDoctors = await _unitOfWork.Prescriptions.GetTopPrescribingDoctorsAsync();

                var doctorDtos = topDoctors.Select(d => new TopPrescribingDoctorDto
                {
                    DoctorName = d.DoctorName,
                    PrescriptionsCount = d.Count
                }).ToList();

                return new ApiResponseDto<IEnumerable<TopPrescribingDoctorDto>>
                {
                    IsSuccess = true,
                    Message = "Top prescribing doctors retrieved successfully.",
                    StatusCode = 200,
                    Data = doctorDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top prescribing doctors.");
                throw;
            }
        }
    }
}