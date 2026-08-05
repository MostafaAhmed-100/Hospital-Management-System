using AutoMapper;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;

namespace HospitalManagementSystem.Mappings.NursingStaffProfiles
{
    public class NurseProfile : Profile
    {
        public NurseProfile()
        {
            CreateMap<Nurse, NurseResponseDto>()
                .ForMember(dest => dest.Shift, opt => opt.MapFrom(src => src.Shift.ToString()))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.FullName : string.Empty));

            CreateMap<CreateNurseDto, Nurse>();
            CreateMap<UpdateNurseDto, Nurse>();
        }
    }
}