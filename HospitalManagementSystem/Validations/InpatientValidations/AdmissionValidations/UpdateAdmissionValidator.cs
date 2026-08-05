using FluentValidation;
using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;

namespace HospitalManagementSystem.Validations.InpatientValidations.AdmissionValidations
{
    public class UpdateAdmissionValidator : AbstractValidator<UpdateAdmissionDto>
    {
        public UpdateAdmissionValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("سبب التنويم مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة التنويم غير صالحة.");
        }
    }
}
