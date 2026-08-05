using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs
{
    public class UpdateOperatingRoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public OperatingRoomStatus Status { get; set; }
    }
}
