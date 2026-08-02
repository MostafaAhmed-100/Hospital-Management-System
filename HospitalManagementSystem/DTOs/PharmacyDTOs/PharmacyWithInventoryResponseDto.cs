namespace HospitalManagementSystem.DTOs.PharmacyDTOs
{
    public class PharmacyWithInventoryResponseDto : PharmacyResponseDto
    {
        public IEnumerable<PharmacyInventoryDto> PharmacyInventories { get; set; }
    }
}
