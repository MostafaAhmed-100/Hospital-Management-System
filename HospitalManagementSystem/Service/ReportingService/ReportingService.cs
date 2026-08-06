using HospitalManagementSystem.DTOs.ReportingDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.ReportingService
{
    public class ReportingService : IReportingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReportingService> _logger;

        public ReportingService(IUnitOfWork unitOfWork, ILogger<ReportingService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponseDto<DashboardReportDto>> GetFullDashboardSummaryAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var revenueData = await _unitOfWork.Dashboards.GetRevenueSummaryAsync(); 

                var bedData = await _unitOfWork.Dashboards.GetBedOccupancyAsync();
                var activityData = await _unitOfWork.Dashboards.GetHospitalActivityAsync();

                var doctorData = await _unitOfWork.Dashboards.GetDoctorUtilizationAsync(startDate, endDate);

                var report = new DashboardReportDto
                {
                    Revenue = new RevenueSummaryDto
                    {
                        TotalRevenue = revenueData.Total,
                        TotalPaid = revenueData.Paid,
                        TotalPending = revenueData.Pending,
                        TotalInvoices = revenueData.Count
                    },
                    Occupancy = new OccupancySummaryDto
                    {
                        TotalBeds = bedData.TotalBeds,
                        OccupiedBeds = bedData.OccupiedBeds,
                        AvailableBeds = bedData.AvailableBeds,
                        TotalOperatingRooms = activityData.TotalOrs,
                        ActiveAdmissions = activityData.ActiveAdmissions
                    },
                    TopDoctors = doctorData.Select(d => new DoctorUtilizationDto
                    {
                        DoctorId = d.Doctor.Id,
                        Specialty = d.Doctor.Specialty?.Name ?? "غير محدد",
                        AppointmentsCount = d.AppointmentsCount
                    })
                };

                return new ApiResponseDto<DashboardReportDto>
                {
                    Message = "تم تحميل بيانات اللوحة بنجاح.",
                    Data = report
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating dashboard report.");
                throw;
            }
        }
    }
}