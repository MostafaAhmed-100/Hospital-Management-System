using AutoMapper;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs;

namespace HospitalManagementSystem.Mappings.OutpatientVisitsProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.ClinicName, opt =>
                    opt.MapFrom(src => src.Clinic != null ? src.Clinic.Name : string.Empty))
                .ForMember(dest => dest.PatientName, opt =>
                    opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty));

            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<UpdateAppointmentDto, Appointment>();
        }
    }
}