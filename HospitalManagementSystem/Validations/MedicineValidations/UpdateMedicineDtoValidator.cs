using FluentValidation;
using HospitalManagementSystem.DTOs.MedicineDTOs;

namespace HospitalManagementSystem.Validations.MedicineValidations
{
    public class UpdateMedicineDtoValidator : AbstractValidator<UpdateMedicineDto>
    {
        public UpdateMedicineDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("رقم الدواء غير صحيح.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الدواء مطلوب.")
                .MaximumLength(150).WithMessage("اسم الدواء يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الدواء لا يمكن أن يكون بالسالب.");
        }
    }
}