using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.PharmacyValidations
{
    public class UpdatePharmacyDtoValidator : AbstractValidator<UpdatePharmacyDto>
    {
        public UpdatePharmacyDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الصيدلية غير صحيح.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الصيدلية مطلوب.")
                .MaximumLength(150).WithMessage("اسم الصيدلية يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("رقم الترخيص مطلوب.")
                .MaximumLength(50).WithMessage("رقم الترخيص يجب ألا يتجاوز 50 حرف.");
        }
    }
}