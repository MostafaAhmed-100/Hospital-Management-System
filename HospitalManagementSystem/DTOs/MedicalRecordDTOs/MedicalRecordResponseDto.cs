namespace HospitalManagementSystem.DTOs.MedicalRecordDTOs
{
    public class MedicalRecordResponseDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Diagnosis { get; set; }
    }
}
