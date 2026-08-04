using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs
{
    public class UpdateAppointmentDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
