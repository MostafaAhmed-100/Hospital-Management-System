namespace HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs
{
    public class UpdatePhysioSessionDto
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string TherapyType { get; set; }
        public string? ProgressNotes { get; set; }
    }
}
