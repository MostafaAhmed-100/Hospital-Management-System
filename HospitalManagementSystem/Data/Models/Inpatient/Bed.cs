using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.Inpatient
{
    public class Bed
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string BedNumber { get; set; }
        public BedStatus Status { get; set; } 
        public bool IsDeleted { get; set; } = false;
        public Room Room { get; set; }
        public ICollection<Admission> Admissions { get; set; }
    }
}
