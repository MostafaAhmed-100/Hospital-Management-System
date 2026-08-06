using FluentValidation;
using HospitalManagementSystem.DTOs.ClinicDTOs;

namespace HospitalManagementSystem.Validations.ClinicValidations
{
    public class CreateClinicDtoValidator : AbstractValidator<CreateClinicDto>
    {
        public CreateClinicDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم العيادة مطلوب.")
                .MaximumLength(100).WithMessage("اسم العيادة يجب ألا يتجاوز 100 حرف.")
                .MinimumLength(2).WithMessage("اسم العيادة يجب أن يحتوي على حرفين على الأقل.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("رقم القسم مطلوب.")
                .GreaterThan(0).WithMessage("رقم القسم غير صحيح.");

            RuleFor(x => x.ClinicType).IsInEnum().WithMessage(" نوع العياده غير صالح")
                .NotNull().WithMessage("نوع العياده مطلوب");
        }
    }
}