using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs
{
    public class BedStatusDistributionDto
    {
        public BedStatus Status { get; set; }
        public int Count { get; set; }
    }
}
