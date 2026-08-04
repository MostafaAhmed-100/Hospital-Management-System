using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.MedicineDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.MedicineValidations
{
    public class CreateMedicineDtoValidator : AbstractValidator<CreateMedicineDto>
    {
        public CreateMedicineDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الدواء مطلوب.")
                .MaximumLength(150).WithMessage("اسم الدواء يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الدواء لا يمكن أن يكون بالسالب.");
        }
    }
}