using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.LabTestDTOs
{
    public class LabTestStatusDistributionDto
    {
        public LabTestStatus Status { get; set; }
        public int Count { get; set; }
    }
}
