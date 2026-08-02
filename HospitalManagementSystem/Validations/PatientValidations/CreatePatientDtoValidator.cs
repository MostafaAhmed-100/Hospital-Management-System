using FluentValidation;
using HospitalManagementSystem.DTOs.PatientDTOs;

namespace HospitalManagementSystem.Validations.PatientValidations
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("اسم المريض مطلوب.")
                .MaximumLength(150).WithMessage("اسم المريض يجب ألا يتجاوز 150 حرف.")
                .MinimumLength(5).WithMessage("اسم المريض يجب أن يحتوي على 5 أحرف على الأقل.");

            RuleFor(x => x.InsuranceId)
                .GreaterThan(0).When(x => x.InsuranceId.HasValue)
                .WithMessage("رقم التأمين غير صحيح.");
        }
    }
}