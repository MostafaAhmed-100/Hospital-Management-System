namespace HospitalManagementSystem.DTOs.ReportingDTOs
{
    public class DashboardReportDto
    {
        public RevenueSummaryDto Revenue { get; set; }
        public OccupancySummaryDto Occupancy { get; set; }
        public IEnumerable<DoctorUtilizationDto> TopDoctors { get; set; }
    }
}
