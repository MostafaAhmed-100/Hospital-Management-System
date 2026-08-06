using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.ClinicDTOs
{
    public class CreateClinicDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public ClinicType ClinicType { get; set; }
    }
}