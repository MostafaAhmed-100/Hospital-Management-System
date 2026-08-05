using FluentValidation;
using HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs;

namespace HospitalManagementSystem.Validations.EmergencyValidators
{
    public class UpdateErVisitValidator : AbstractValidator<UpdateErVisitDto>
    {
        public UpdateErVisitValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("معرف الزيارة غير صحيح.");
            RuleFor(x => x.ChiefComplaint).NotEmpty().WithMessage("الشكوى الأساسية مطلوبة.");
            RuleFor(x => x.TriageLevel).IsInEnum().WithMessage("مستوى الفرز غير صحيح.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة الزيارة غير صحيحة.");
        }
    }
}
