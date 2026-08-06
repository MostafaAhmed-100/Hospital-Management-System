using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.LabTestDTOs
{
    public class UpdateLabTestResultDto
    {
        public int Id { get; set; }
        public string? Result { get; set; }
        public LabTestStatus Status { get; set; }
    }
}
