using FluentValidation;
using HospitalManagementSystem.DTOs.LabTestDTOs;

namespace HospitalManagementSystem.Validations.LabTestValidators
{
    public class CreateLabTestValidator : AbstractValidator<CreateLabTestDto>
    {
        public CreateLabTestValidator()
        {
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("يجب ربط التحليل بسجل طبي.");
            RuleFor(x => x.TestName).NotEmpty().WithMessage("اسم التحليل مطلوب.").MaximumLength(150).WithMessage("الاسم طويل جداً.");
            RuleFor(x => x.TestDate).NotEmpty().WithMessage("تاريخ التحليل مطلوب.");
        }
    }
}
