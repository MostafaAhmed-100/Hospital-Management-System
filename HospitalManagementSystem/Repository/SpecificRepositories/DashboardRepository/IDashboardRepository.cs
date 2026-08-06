using HospitalManagementSystem.Data.Models.Clinics_Doctors;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DashboardRepository
{
    public interface IDashboardRepository
    {
        Task<(decimal Total, decimal Paid, decimal Pending, int Count)> GetRevenueSummaryAsync();
        Task<(int TotalBeds, int OccupiedBeds, int AvailableBeds)> GetBedOccupancyAsync();
        Task<(int TotalOrs, int ActiveAdmissions)> GetHospitalActivityAsync();
        Task<IEnumerable<(Doctor Doctor, int AppointmentsCount)>> GetDoctorUtilizationAsync(DateTime? startDate, DateTime? endDate);
    }
}
