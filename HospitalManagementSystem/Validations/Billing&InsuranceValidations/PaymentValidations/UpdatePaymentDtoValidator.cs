using FluentValidation;
using HospitalManagementSystem.DTOs.PaymentDTOs;

namespace HospitalManagementSystem.Validations.PaymentValidations
{
    public class UpdatePaymentDtoValidator : AbstractValidator<UpdatePaymentDto>
    {
        public UpdatePaymentDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الدفعة غير صحيح.");
            RuleFor(x => x.InvoiceId).GreaterThan(0).WithMessage("رقم الفاتورة غير صحيح.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("قيمة الدفع يجب أن تكون أكبر من صفر.");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("طريقة الدفع مطلوبة.")
                .MaximumLength(50).WithMessage("طريقة الدفع يجب ألا تتجاوز 50 حرف.");
        }
    }
}