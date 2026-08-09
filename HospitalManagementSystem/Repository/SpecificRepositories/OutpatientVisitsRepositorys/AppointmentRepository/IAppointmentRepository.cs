using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.AppointmentRepository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment?>> GetUpcomingAppointmentsByDoctorAsync(int doctorId);
        Task<bool> HasConflictAsync(int doctorId, DateTime requestedTime);
        Task<IEnumerable<(AppointmentStatus Status, int Count)>> GetAppointmentsDistributionByStatusAsync();
        Task<int> GetTodayAppointmentsCountAsync();
    }
}
