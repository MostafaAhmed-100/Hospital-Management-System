namespace HospitalManagementSystem.DTOs.PrescriptionDTOs
{
    public class PrescriptionWithItemsResponseDto
    {
        public IEnumerable<PrescriptionItemResponseDto> PrescriptionItems { get; set; }
    }
}
