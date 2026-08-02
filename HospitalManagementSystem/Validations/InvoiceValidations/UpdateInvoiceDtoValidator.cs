using FluentValidation;
using HospitalManagementSystem.DTOs.InvoiceDTOs;

namespace HospitalManagementSystem.Validations.InvoiceValidations
{
    public class UpdateInvoiceDtoValidator : AbstractValidator<UpdateInvoiceDto>
    {
        public UpdateInvoiceDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الفاتورة غير صحيح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");
            RuleFor(x => x.AppointmentId).GreaterThan(0).WithMessage("رقم الحجز غير صحيح.");

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("قيمة الفاتورة لا يمكن أن تكون بالسالب.");

            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة الفاتورة غير صحيحة.");
        }
    }
}