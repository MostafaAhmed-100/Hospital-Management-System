namespace HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs
{
    public class PhysioSessionResponseDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int TherapistId { get; set; }
        public string TherapistName { get; set; }
        public int RecordId { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string TherapyType { get; set; }
        public string? ProgressNotes { get; set; }
    }
}
