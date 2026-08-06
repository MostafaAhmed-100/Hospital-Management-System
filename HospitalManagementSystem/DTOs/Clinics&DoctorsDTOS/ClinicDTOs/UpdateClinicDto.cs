using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.ClinicDTOs
{
    public class UpdateClinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public ClinicType ClinicType { get; set; }
    }
}
