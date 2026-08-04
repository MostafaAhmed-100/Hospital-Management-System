namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyDTOs
{
    public class PharmacyWithInventoryResponseDto : PharmacyResponseDto
    {
        public IEnumerable<PharmacyInventoryDto> PharmacyInventories { get; set; }
    }
}
