namespace HospitalManagementSystem.DTOs.ReportingDTOs
{
    public class OccupancySummaryDto
    {
        public int TotalBeds { get; set; }
        public int OccupiedBeds { get; set; }
        public int AvailableBeds { get; set; }
        public int TotalOperatingRooms { get; set; }
        public int ActiveAdmissions { get; set; }
    }
}
