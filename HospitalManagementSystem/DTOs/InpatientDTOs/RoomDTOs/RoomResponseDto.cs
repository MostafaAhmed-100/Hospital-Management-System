namespace HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
    }
}
