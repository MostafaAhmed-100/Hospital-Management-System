namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs
{
    public class PharmacySaleWithItemsResponseDto : PharmacySaleResponseDto
    {
        public IEnumerable<SaleItemDto> SaleItems { get; set; }
    }
}
