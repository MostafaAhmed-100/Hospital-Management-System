namespace HospitalManagementSystem.DTOs.ReportingDTOs
{
    public class DoctorUtilizationDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public int AppointmentsCount { get; set; }
    }
}
