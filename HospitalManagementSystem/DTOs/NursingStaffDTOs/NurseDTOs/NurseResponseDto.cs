namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs
{
    public class NurseResponseDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string LicenseNumber { get; set; }
        public string Shift { get; set; }
        public string WardSpecialization { get; set; }
    }
}
