using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Physiotherapy
{
    public class PhysioSession
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int TherapistId { get; set; }
        public int RecordId { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string TherapyType { get; set; }
        public string? ProgressNotes { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Patient Patient { get; set; }
        public Therapist Therapist { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}
