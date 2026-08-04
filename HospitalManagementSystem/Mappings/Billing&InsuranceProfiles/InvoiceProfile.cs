using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.DTOs.InvoiceDTOs;

namespace HospitalManagementSystem.Mappings
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceResponseDto>()
                .ForMember(dest => dest.PatientName, opt =>
                    opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty));

            CreateMap<Invoice, InvoiceWithPaymentsResponseDto>();

            CreateMap<Payment, InvoicePaymentDto>();

            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<UpdateInvoiceDto, Invoice>();
        }
    }
}