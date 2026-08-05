namespace HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs
{
    public class SurgeryRecordResponseDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int LeadSurgeonId { get; set; }
        public string LeadSurgeonName { get; set; }
        public int OperatingRoomId { get; set; }
        public string OperatingRoomNumber { get; set; }
        public int RecordId { get; set; }
        public string SurgeryType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
