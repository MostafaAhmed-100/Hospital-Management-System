using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs;

namespace HospitalManagementSystem.Mappings.PharmacysProfiles
{
    public class PharmacyInventoryProfile : Profile
    {
        public PharmacyInventoryProfile()
        {
            CreateMap<PharmacyInventory, PharmacyInventoryResponseDto>()
                .ForMember(dest => dest.PharmacyName, opt =>
                    opt.MapFrom(src => src.Pharmacy != null ? src.Pharmacy.Name : string.Empty))
                .ForMember(dest => dest.MedicineName, opt =>
                    opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : string.Empty));

            CreateMap<CreatePharmacyInventoryDto, PharmacyInventory>();
            CreateMap<UpdatePharmacyInventoryDto, PharmacyInventory>();
        }
    }
}