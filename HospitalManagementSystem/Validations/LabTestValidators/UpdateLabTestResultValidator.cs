using FluentValidation;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.LabTestDTOs;

namespace HospitalManagementSystem.Validations.LabTestValidators
{
    public class UpdateLabTestResultValidator : AbstractValidator<UpdateLabTestResultDto>
    {
        public UpdateLabTestResultValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة التحليل غير صالحة.");
            RuleFor(x => x.Result)
                .NotEmpty().When(x => x.Status == LabTestStatus.Completed)
                .WithMessage("يجب إدخال النتيجة عندما تكون حالة التحليل مكتملة.");
        }
    }
}
