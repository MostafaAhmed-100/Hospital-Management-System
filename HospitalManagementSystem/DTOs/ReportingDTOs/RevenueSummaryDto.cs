namespace HospitalManagementSystem.DTOs.ReportingDTOs
{
    public class RevenueSummaryDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalPending { get; set; }
        public int TotalInvoices { get; set; }
    }
}
