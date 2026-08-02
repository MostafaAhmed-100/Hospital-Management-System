using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.ClinicDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class ClinicProfile : Profile
    {
        public ClinicProfile()
        {
            CreateMap<Clinic, ClinicResponseDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateClinicDto, Clinic>();
            CreateMap<UpdateClinicDto, Clinic>();
        }
    }
}