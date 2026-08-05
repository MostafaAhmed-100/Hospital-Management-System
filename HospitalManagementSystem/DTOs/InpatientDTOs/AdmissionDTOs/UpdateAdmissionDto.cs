using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs
{
    public class UpdateAdmissionDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public AdmissionStatus Status { get; set; }
        public DateTime? DischargeDate { get; set; }
    }
}
