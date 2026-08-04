namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs
{
    public class PrescriptionWithItemsResponseDto
    {
        public IEnumerable<PrescriptionItemResponseDto> PrescriptionItems { get; set; }
    }
}
