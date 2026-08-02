using FluentValidation;
using HospitalManagementSystem.DTOs.InsuranceProviderDTOs;

namespace HospitalManagementSystem.Validations.InsuranceProviderValidations
{
    public class UpdateInsuranceProviderDtoValidator : AbstractValidator<UpdateInsuranceProviderDto>
    {
        public UpdateInsuranceProviderDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الشركة غير صحيح.");

            RuleFor(x => x.ProviderName)
                .NotEmpty().WithMessage("اسم شركة التأمين مطلوب.")
                .MaximumLength(150).WithMessage("اسم الشركة يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.CoveragePercentage)
                .InclusiveBetween(0, 100).WithMessage("نسبة التغطية يجب أن تكون بين 0 و 100.");
        }
    }
}