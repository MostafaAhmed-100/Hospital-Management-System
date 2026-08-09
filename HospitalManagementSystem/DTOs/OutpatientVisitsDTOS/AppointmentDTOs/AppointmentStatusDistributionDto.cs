using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs
{
    public class AppointmentStatusDistributionDto
    {
        public AppointmentStatus Status { get; set; }
        public int Count { get; set; }
    }
}
