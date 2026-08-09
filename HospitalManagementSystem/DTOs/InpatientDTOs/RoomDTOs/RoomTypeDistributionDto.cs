using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs
{
    public class RoomTypeDistributionDto
    {
        public RoomType RoomType { get; set; }
        public int Count { get; set; }
    }
}
