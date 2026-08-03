using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Surgery
{
    public class SurgeryRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; } 
        public int LeadSurgeonId { get; set; } 
        public int OperatingRoomId { get; set; } 
        public int RecordId { get; set; } 
        public string SurgeryType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SurgeryStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Patient Patient { get; set; }
        public Doctor LeadSurgeon { get; set; }
        public OperatingRoom OperatingRoom { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public ICollection<SurgeryTeam> SurgeryTeams { get; set; }
    }
}