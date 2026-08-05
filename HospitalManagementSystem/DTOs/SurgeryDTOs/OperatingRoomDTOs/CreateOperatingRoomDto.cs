using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs
{
    public class CreateOperatingRoomDto
    {
        public string RoomNumber { get; set; }
        public OperatingRoomStatus Status { get; set; } = OperatingRoomStatus.Available;
    }
}
