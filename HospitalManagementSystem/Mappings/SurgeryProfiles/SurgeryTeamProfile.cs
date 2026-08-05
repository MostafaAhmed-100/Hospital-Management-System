using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs;

namespace HospitalManagementSystem.Mappings.SurgeryProfiles
{
    public class SurgeryTeamProfile : Profile
    {
        public SurgeryTeamProfile()
        {
            CreateMap<SurgeryTeam, SurgeryTeamResponseDto>()
                .ForMember(dest => dest.RoleInSurgery, opt => opt.MapFrom(src => src.RoleInSurgery.ToString()))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.FullName : string.Empty));

            CreateMap<CreateSurgeryTeamDto, SurgeryTeam>();
            CreateMap<UpdateSurgeryTeamDto, SurgeryTeam>();
        }
    }
}
