using AutoMapper;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.DTOs.DepartmentDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Department, DepartmentWithDetailsResponseDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
}