using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacyDTOs;

namespace HospitalManagementSystem.Validations.PharmacyValidations
{
    public class CreatePharmacyDtoValidator : AbstractValidator<CreatePharmacyDto>
    {
        public CreatePharmacyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الصيدلية مطلوب.")
                .MaximumLength(150).WithMessage("اسم الصيدلية يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("رقم الترخيص مطلوب.")
                .MaximumLength(50).WithMessage("رقم الترخيص يجب ألا يتجاوز 50 حرف.");
        }
    }
}