using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.Surgery
{
    public class OperatingRoom
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public OperatingRoomStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<SurgeryRecord> Surgeries { get; set; }
    }
}