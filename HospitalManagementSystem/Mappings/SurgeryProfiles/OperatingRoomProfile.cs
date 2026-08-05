using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;

namespace HospitalManagementSystem.Mappings.SurgeryProfiles
{
    public class OperatingRoomProfile : Profile
    {
        public OperatingRoomProfile()
        {
            CreateMap<OperatingRoom, OperatingRoomResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateOperatingRoomDto, OperatingRoom>();
            CreateMap<UpdateOperatingRoomDto, OperatingRoom>();
        }
    }
}
