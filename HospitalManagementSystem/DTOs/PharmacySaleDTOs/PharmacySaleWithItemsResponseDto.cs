namespace HospitalManagementSystem.DTOs.PharmacySaleDTOs
{
    public class PharmacySaleWithItemsResponseDto : PharmacySaleResponseDto
    {
        public IEnumerable<SaleItemDto> SaleItems { get; set; }
    }
}
