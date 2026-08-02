using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.DTOs.PaymentDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<UpdatePaymentDto, Payment>();
        }
    }
}