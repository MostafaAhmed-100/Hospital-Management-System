namespace HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs
{
    public class CreateAdmissionDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int BedId { get; set; }
        public int RecordId { get; set; }
        public string Reason { get; set; }
    }
}
