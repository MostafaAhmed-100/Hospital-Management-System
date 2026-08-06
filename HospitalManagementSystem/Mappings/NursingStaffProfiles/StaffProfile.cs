using AutoMapper;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;

namespace HospitalManagementSystem.Mappings.NursingStaffProfiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<CreateStaffDto, Staff>();
            CreateMap<UpdateStaffDto, Staff>();
        }
    }
}