using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.DoctorDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorResponseDto>();
                

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();
        }
    }
}