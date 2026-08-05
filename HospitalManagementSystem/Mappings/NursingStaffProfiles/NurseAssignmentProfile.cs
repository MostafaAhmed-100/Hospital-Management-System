using AutoMapper;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;

namespace HospitalManagementSystem.Mappings.NursingStaffProfiles
{
    public class NurseAssignmentProfile : Profile
    {
        public NurseAssignmentProfile()
        {
            CreateMap<NurseAssignment, NurseAssignmentResponseDto>()
                .ForMember(dest => dest.Shift, opt => opt.MapFrom(src => src.Shift.ToString()))
                .ForMember(dest => dest.NurseName, opt => opt.MapFrom(src => src.Nurse != null && src.Nurse.Staff != null ? src.Nurse.Staff.FullName : string.Empty));

            CreateMap<CreateNurseAssignmentDto, NurseAssignment>();
            CreateMap<UpdateNurseAssignmentDto, NurseAssignment>();
        }
    }
}