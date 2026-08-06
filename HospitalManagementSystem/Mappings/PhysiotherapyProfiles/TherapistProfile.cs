using AutoMapper;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;

namespace HospitalManagementSystem.Mappings.PhysiotherapyProfiles
{
    public class TherapistProfile : Profile
    {
        public TherapistProfile()
        {
            CreateMap<Therapist, TherapistResponseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            CreateMap<CreateTherapistDto, Therapist>();
            CreateMap<UpdateTherapistDto, Therapist>();
        }
    }
}
