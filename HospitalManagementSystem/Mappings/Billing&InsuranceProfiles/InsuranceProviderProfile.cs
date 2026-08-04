using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.DTOs.InsuranceProviderDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class InsuranceProviderProfile : Profile
    {
        public InsuranceProviderProfile()
        {
            CreateMap<InsuranceProvider, InsuranceProviderResponseDto>();
            CreateMap<CreateInsuranceProviderDto, InsuranceProvider>();
            CreateMap<UpdateInsuranceProviderDto, InsuranceProvider>();
            CreateMap<Patient, ProviderPatientDto>();
            CreateMap<InsuranceProvider, InsuranceProviderWithPatientsResponseDto>();
        }
    }
}