using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DashboardRepository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(decimal Total, decimal Paid, decimal Pending, int Count)> GetRevenueSummaryAsync()
        {
            var invoices = await _context.Invoices.AsNoTracking().ToListAsync();

            var total = invoices.Sum(i => i.Amount);
            var paid = invoices.Where(i => i.Status == InvoiceStatus.Paid).Sum(i => i.Amount);
            var pending = invoices.Where(i => i.Status == InvoiceStatus.Pending).Sum(i => i.Amount);
            var count = invoices.Count;

            return (total, paid, pending, count);
        }

        public async Task<(int TotalBeds, int OccupiedBeds, int AvailableBeds)> GetBedOccupancyAsync()
        {
            var beds = await _context.Beds.AsNoTracking().ToListAsync();

            int total = beds.Count;
            int occupied = beds.Count(b => b.Status == BedStatus.Occupied);
            int available = total - occupied;

            return (total, occupied, available);
        }

        public async Task<(int TotalOrs, int ActiveAdmissions)> GetHospitalActivityAsync()
        {
            int totalOrs = await _context.OperatingRooms.CountAsync();
            int activeAdmissions = await _context.Admissions.CountAsync(a => a.DischargeDate == null);

            return (totalOrs, activeAdmissions);
        }

        public async Task<IEnumerable<(Doctor Doctor, int AppointmentsCount)>> GetDoctorUtilizationAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Appointments.AsNoTracking().AsQueryable();

            if (startDate.HasValue)
                query = query.Where(a => a.AppointmentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(a => a.AppointmentDate <= endDate.Value);

            var utilization = await query
                .Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                .GroupBy(a => a.Doctor)
                .Select(g => new { Doctor = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return utilization.Select(u => (u.Doctor, u.Count));
        }
    }
}