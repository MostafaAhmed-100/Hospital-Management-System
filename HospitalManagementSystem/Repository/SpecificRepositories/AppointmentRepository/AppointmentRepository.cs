using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.AppointmentRepository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Appointment?>> GetUpcomingAppointmentsByDoctorAsync(int doctorId)
        {
            var Appointments = await _AppDbcontext.Appointments
                .Include(x => x.Patient)
                .Where(x => x.DoctorId == doctorId && x.AppointmentDate >= DateTime.UtcNow)
                .ToListAsync();
            return Appointments;
        }

        public async Task<bool> HasConflictAsync(int doctorId, DateTime requestedTime)
        {
            var Conflict = await _AppDbcontext.Appointments
                .AnyAsync(x => x.DoctorId == doctorId && x.AppointmentDate == requestedTime);
            return Conflict;
        }
    }
}
