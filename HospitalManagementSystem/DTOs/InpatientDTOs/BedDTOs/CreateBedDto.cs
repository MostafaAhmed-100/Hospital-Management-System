using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs
{
    public class CreateBedDto
    {
        public int RoomId { get; set; }
        public string BedNumber { get; set; }
        public BedStatus Status { get; set; } = BedStatus.Available;
    }
}
