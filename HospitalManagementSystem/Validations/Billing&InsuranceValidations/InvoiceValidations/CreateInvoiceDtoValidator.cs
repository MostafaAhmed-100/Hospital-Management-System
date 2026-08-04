using FluentValidation;
using HospitalManagementSystem.DTOs.InvoiceDTOs;

namespace HospitalManagementSystem.Validations.InvoiceValidations
{
    public class CreateInvoiceDtoValidator : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceDtoValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");
            RuleFor(x => x.AppointmentId).GreaterThan(0).WithMessage("رقم الحجز غير صحيح.");

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("قيمة الفاتورة لا يمكن أن تكون بالسالب.");
        }
    }
}