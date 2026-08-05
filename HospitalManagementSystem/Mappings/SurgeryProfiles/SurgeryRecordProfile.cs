using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;

namespace HospitalManagementSystem.Mappings.SurgeryProfiles
{
    public class SurgeryRecordProfile : Profile
    {
        public SurgeryRecordProfile()
        {
            CreateMap<SurgeryRecord, SurgeryRecordResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.LeadSurgeonName, opt => opt.MapFrom(src => src.LeadSurgeon != null ? src.LeadSurgeon.FullName : string.Empty))
                .ForMember(dest => dest.OperatingRoomNumber, opt => opt.MapFrom(src => src.OperatingRoom != null ? src.OperatingRoom.RoomNumber : string.Empty));

            CreateMap<CreateSurgeryRecordDto, SurgeryRecord>();
            CreateMap<UpdateSurgeryRecordDto, SurgeryRecord>();
        }
    }
}