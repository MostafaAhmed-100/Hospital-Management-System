using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.Inpatient
{
    public class Room
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Department Department { get; set; }
        public ICollection<Bed> Beds { get; set; }
    }
}
