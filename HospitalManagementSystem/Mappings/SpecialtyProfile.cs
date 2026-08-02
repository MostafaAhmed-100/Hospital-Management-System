using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.SpecialtyDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class SpecialtyProfile : Profile
    {
        public SpecialtyProfile()
        {
            CreateMap<Specialty, SpecialtyResponseDto>();
            CreateMap<Specialty, SpecialtyWithDoctorsResponseDto>();

            CreateMap<CreateSpecialtyDto, Specialty>();
            CreateMap<UpdateSpecialtyDto, Specialty>();
        }
    }
}