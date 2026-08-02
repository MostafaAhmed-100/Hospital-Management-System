using FluentValidation;
using HospitalManagementSystem.DTOs.PrescriptionItemDTOs;

namespace HospitalManagementSystem.Validations.PrescriptionItemValidations
{
    public class UpdatePrescriptionItemDtoValidator : AbstractValidator<UpdatePrescriptionItemDto>
    {
        public UpdatePrescriptionItemDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم العنصر غير صحيح.");
            RuleFor(x => x.PrescriptionId).GreaterThan(0).WithMessage("رقم الروشتة غير صحيح.");
            RuleFor(x => x.MedicineId).GreaterThan(0).WithMessage("رقم الدواء غير صحيح.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من صفر.");

            RuleFor(x => x.Dosage)
                .NotEmpty().WithMessage("تفاصيل الجرعة مطلوبة.")
                .MaximumLength(200).WithMessage("تفاصيل الجرعة يجب ألا تتجاوز 200 حرف.");
        }
    }
}