using AutoMapper;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.PatientDTOs;

namespace HospitalManagementSystem.Mappings.OutpatientVisitsProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientResponseDto>()
                .ForMember(dest => dest.InsuranceProviderName, opt =>
                    opt.MapFrom(src => src.InsuranceProvider != null ? src.InsuranceProvider.ProviderName : null));

            CreateMap<Patient, PatientWithMedicalHistoryResponseDto>();

            CreateMap<MedicalRecord, MedicalRecordDto>();

            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();
        }
    }
}