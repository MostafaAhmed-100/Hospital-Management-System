using FluentValidation;
using HospitalManagementSystem.DTOs.SpecialtyDTOs;

namespace HospitalManagementSystem.Validations.SpecialtyValidations
{
    public class UpdateSpecialtyDtoValidator : AbstractValidator<UpdateSpecialtyDto>
    {
        public UpdateSpecialtyDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("رقم التخصص غير صحيح.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التخصص مطلوب.")
                .MaximumLength(100).WithMessage("اسم التخصص يجب ألا يتجاوز 100 حرف.")
                .MinimumLength(2).WithMessage("اسم التخصص يجب أن يحتوي على حرفين على الأقل.");
        }
    }
}