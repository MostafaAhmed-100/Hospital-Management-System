using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacyDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class PharmacyProfile : Profile
    {
        public PharmacyProfile()
        {
            CreateMap<Pharmacy, PharmacyResponseDto>();
            CreateMap<Pharmacy, PharmacyWithInventoryResponseDto>();

            CreateMap<PharmacyInventory, PharmacyInventoryDto>()
                .ForMember(dest => dest.MedicineName, opt =>
                    opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : string.Empty));

            CreateMap<CreatePharmacyDto, Pharmacy>();
            CreateMap<UpdatePharmacyDto, Pharmacy>();
        }
    }
}