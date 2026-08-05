using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs
{
    public class UpdateBedDto : CreateBedDto
    {
        public int Id { get; set; }
    }
}
