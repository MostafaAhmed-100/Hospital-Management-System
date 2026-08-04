using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs;

namespace HospitalManagementSystem.Mappings.PharmacysProfiles
{
    public class SaleItemProfile : Profile
    {
        public SaleItemProfile()
        {
            CreateMap<SaleItem, SaleItemResponseDto>()
                .ForMember(dest => dest.MedicineName, opt =>
                    opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : string.Empty));

            CreateMap<CreateSaleItemDto, SaleItem>();
            CreateMap<UpdateSaleItemDto, SaleItem>();
        }
    }
}