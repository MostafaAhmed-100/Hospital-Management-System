namespace HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs
{
    public class BedResponseDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string BedNumber { get; set; }
        public string Status { get; set; }
    }
}
