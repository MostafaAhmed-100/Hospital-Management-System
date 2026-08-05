using AutoMapper;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;

namespace HospitalManagementSystem.Mappings.InpatientProfiles
{
    public class BedProfile : Profile
    {
        public BedProfile()
        {
            CreateMap<Bed, BedResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room != null ? src.Room.RoomNumber : string.Empty));

            CreateMap<CreateBedDto, Bed>();
            CreateMap<UpdateBedDto, Bed>();
        }
    }
}
