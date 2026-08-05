namespace HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs
{
    public class AdmissionResponseDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int BedId { get; set; }
        public string BedNumber { get; set; }
        public int RecordId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
