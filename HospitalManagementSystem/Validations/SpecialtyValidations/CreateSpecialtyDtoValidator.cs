using FluentValidation;
using HospitalManagementSystem.DTOs.SpecialtyDTOs;

namespace HospitalManagementSystem.Validations.SpecialtyValidations
{
    public class CreateSpecialtyDtoValidator : AbstractValidator<CreateSpecialtyDto>
    {
        public CreateSpecialtyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التخصص مطلوب.")
                .MaximumLength(100).WithMessage("اسم التخصص يجب ألا يتجاوز 100 حرف.")
                .MinimumLength(2).WithMessage("اسم التخصص يجب أن يحتوي على حرفين على الأقل.");
        }
    }
}