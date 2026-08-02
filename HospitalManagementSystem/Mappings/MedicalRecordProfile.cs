using AutoMapper;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.MedicalRecordDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class MedicalRecordProfile : Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordResponseDto>()
                .ForMember(dest => dest.PatientName, opt =>
                    opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty));

            CreateMap<CreateMedicalRecordDto, MedicalRecord>();
            CreateMap<UpdateMedicalRecordDto, MedicalRecord>();
        }
    }
}