namespace HospitalManagementSystem.DTOs.LabTestDTOs
{
    public class LabTestResponseDto
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public string? Result { get; set; }
        public string Status { get; set; }
    }
}
