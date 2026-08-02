using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PharmacySaleDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class PharmacySaleProfile : Profile
    {
        public PharmacySaleProfile()
        {
            CreateMap<PharmacySale, PharmacySaleResponseDto>()
                .ForMember(dest => dest.PharmacyName, opt =>
                    opt.MapFrom(src => src.Pharmacy != null ? src.Pharmacy.Name : string.Empty))
                .ForMember(dest => dest.PatientName, opt =>
                    opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty));

            CreateMap<PharmacySale, PharmacySaleWithItemsResponseDto>();
            CreateMap<SaleItem, SaleItemDto>();

            CreateMap<CreatePharmacySaleDto, PharmacySale>();
            CreateMap<UpdatePharmacySaleDto, PharmacySale>();
        }
    }
}