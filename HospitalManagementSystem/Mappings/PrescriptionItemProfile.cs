using AutoMapper;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.DTOs.PrescriptionItemDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class PrescriptionItemProfile : Profile
    {
        public PrescriptionItemProfile()
        {
            CreateMap<PrescriptionItem, PrescriptionItemResponseDto>()
                .ForMember(dest => dest.MedicineName, opt =>
                    opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : string.Empty));

            CreateMap<CreatePrescriptionItemDto, PrescriptionItem>();
            CreateMap<UpdatePrescriptionItemDto, PrescriptionItem>();
        }
    }
}