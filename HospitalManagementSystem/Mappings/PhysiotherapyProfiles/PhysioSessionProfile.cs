using AutoMapper;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;

namespace HospitalManagementSystem.Mappings.PhysiotherapyProfiles
{
    public class PhysioSessionProfile : Profile
    {
        public PhysioSessionProfile()
        {
            CreateMap<PhysioSession, PhysioSessionResponseDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.TherapistName, opt => opt.MapFrom(src => src.Therapist != null ? src.Therapist.FullName : string.Empty));

            CreateMap<CreatePhysioSessionDto, PhysioSession>();
            CreateMap<UpdatePhysioSessionDto, PhysioSession>();
        }
    }
}
