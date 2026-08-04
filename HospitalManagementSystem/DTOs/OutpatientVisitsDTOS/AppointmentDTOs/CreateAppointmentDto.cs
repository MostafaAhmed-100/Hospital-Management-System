namespace HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs
{
    public class CreateAppointmentDto
    {
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
