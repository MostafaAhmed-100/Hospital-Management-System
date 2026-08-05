using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs
{
    public class CreateRoomDto
    {
        public int DepartmentId { get; set; }
        public string RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
    }
}
