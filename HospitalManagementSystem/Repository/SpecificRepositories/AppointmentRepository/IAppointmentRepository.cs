using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.AppointmentRepository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment?>> GetUpcomingAppointmentsByDoctorAsync(int doctorId);
        Task<bool> HasConflictAsync(int doctorId, DateTime requestedTime);
    }
}
