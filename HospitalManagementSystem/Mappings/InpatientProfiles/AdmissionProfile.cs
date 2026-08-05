using AutoMapper;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;

namespace HospitalManagementSystem.Mappings.InpatientProfiles
{
    public class AdmissionProfile : Profile
    {
        public AdmissionProfile()
        {
            CreateMap<Admission, AdmissionResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.FullName : string.Empty))
                .ForMember(dest => dest.BedNumber, opt => opt.MapFrom(src => src.Bed != null ? src.Bed.BedNumber : string.Empty));

            CreateMap<CreateAdmissionDto, Admission>();
            CreateMap<UpdateAdmissionDto, Admission>();
        }
    }
}
