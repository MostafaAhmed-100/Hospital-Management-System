namespace HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.MedicalRecordDTOs
{
    public class CreateMedicalRecordDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
    }
}
