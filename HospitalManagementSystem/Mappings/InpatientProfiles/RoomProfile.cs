using AutoMapper;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;

namespace HospitalManagementSystem.Mappings.InpatientProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomResponseDto>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.ToString()))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>();
        }
    }
}