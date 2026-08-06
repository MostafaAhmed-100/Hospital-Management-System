using AutoMapper;
using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.DTOs.LabTestDTOs;

namespace HospitalManagementSystem.Mappings.LabTestProfiles
{
    public class LabTestProfile : Profile
    {
        public LabTestProfile()
        {
            CreateMap<LabTest, LabTestResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateLabTestDto, LabTest>();
            CreateMap<UpdateLabTestResultDto, LabTest>();
        }
    }
}
