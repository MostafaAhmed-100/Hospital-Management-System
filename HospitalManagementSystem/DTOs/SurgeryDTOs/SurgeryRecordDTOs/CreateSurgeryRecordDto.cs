using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs
{
    public class CreateSurgeryRecordDto
    {
        public int PatientId { get; set; }
        public int LeadSurgeonId { get; set; }
        public int OperatingRoomId { get; set; }
        public int RecordId { get; set; }
        public string SurgeryType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SurgeryStatus Status { get; set; } = SurgeryStatus.Scheduled;
    }
}
