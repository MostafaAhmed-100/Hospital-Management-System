using FluentValidation;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;

namespace HospitalManagementSystem.Validations.EmergencyValidators
{
    public class CreateErVisitValidator : AbstractValidator<CreateErVisitDto>
    {
        public CreateErVisitValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("يجب تحديد المريض.");
            RuleFor(x => x.AttendingDoctorId).GreaterThan(0).WithMessage("يجب تحديد الطبيب المعالج.");
            RuleFor(x => x.ChiefComplaint).NotEmpty().WithMessage("الشكوى الأساسية مطلوبة.");
            RuleFor(x => x.TriageLevel).IsInEnum().WithMessage("مستوى الفرز غير صحيح.");
        }
    }
}
