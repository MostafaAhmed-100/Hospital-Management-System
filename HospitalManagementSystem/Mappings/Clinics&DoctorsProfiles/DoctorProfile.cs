using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.DoctorDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorResponseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.Name : string.Empty));

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();
        }
    }
}