using FluentValidation;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;

namespace HospitalManagementSystem.Validations.PhysiotherapyValidators.PhysioSessionValidators
{
    public class UpdatePhysioSessionValidator : AbstractValidator<UpdatePhysioSessionDto>
    {
        public UpdatePhysioSessionValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.TherapistId).GreaterThan(0).WithMessage("يجب تحديد المعالج.");
            RuleFor(x => x.SessionDate).NotEmpty().WithMessage("تاريخ الجلسة مطلوب.");
            RuleFor(x => x.DurationMinutes).GreaterThan(0).WithMessage("مدة الجلسة يجب أن تكون أكبر من صفر.");
            RuleFor(x => x.TherapyType).NotEmpty().WithMessage("نوع العلاج مطلوب.");
        }
    }
}
