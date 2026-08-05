using AutoMapper;
using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;

namespace HospitalManagementSystem.Mappings.EmergencyProfiles.ErVisitProfile
{
    public class ErVisitProfile : Profile
    {
        public ErVisitProfile()
        {
            CreateMap<ErVisit, ErVisitDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.AttendingDoctor.FullName));

            CreateMap<CreateErVisitDto, ErVisit>();
            CreateMap<UpdateErVisitDto, ErVisit>();
        }
    }
}
