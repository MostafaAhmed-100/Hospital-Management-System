using FluentValidation;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.PhysioSessionDTOs;

namespace HospitalManagementSystem.Validations.PhysiotherapyValidators.PhysioSessionValidators
{
    public class CreatePhysioSessionValidator : AbstractValidator<CreatePhysioSessionDto>
    {
        public CreatePhysioSessionValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("يجب تحديد المريض.");
            RuleFor(x => x.TherapistId).GreaterThan(0).WithMessage("يجب تحديد المعالج.");
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("يجب ربط الجلسة بسجل طبي (روشتة أو كشف).");
            RuleFor(x => x.SessionDate).NotEmpty().WithMessage("تاريخ الجلسة مطلوب.");
            RuleFor(x => x.DurationMinutes).GreaterThan(0).WithMessage("مدة الجلسة يجب أن تكون أكبر من صفر.");
            RuleFor(x => x.TherapyType).NotEmpty().WithMessage("نوع العلاج مطلوب.");
        }
    }
}
