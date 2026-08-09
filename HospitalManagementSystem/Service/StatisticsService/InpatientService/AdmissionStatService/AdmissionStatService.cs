using AutoMapper;
using HospitalManagementSystem.DTOs.DoctorDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.StatisticsService.InpatientService.AdmissionStatService
{
    public class AdmissionStatService : IAdmissionStatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AdmissionStatService> _logger;

        public AdmissionStatService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AdmissionStatService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<int>> GetActiveAdmissionsCountAsync()
        {
            try
            {
                var count = await _unitOfWork.Admissions.GetActiveAdmissionsCountAsync();

                return new ApiResponseDto<int>
                {
                    IsSuccess = true,
                    Message = "Active admissions count retrieved successfully.",
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving active admissions count.");
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<DoctorResponseDto>>> GetTopAdmittingDoctorsAsync()
        {
            try
            {
                var doctors = await _unitOfWork.Admissions.GetTopAdmittingDoctorsAsync();
                var doctorDtos = _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);

                return new ApiResponseDto<IEnumerable<DoctorResponseDto>>
                {
                    IsSuccess = true,
                    Message = "Top admitting doctors retrieved successfully.",
                    StatusCode = 200,
                    Data = doctorDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving top admitting doctors.");
                throw;
            }
        }
    }
}
