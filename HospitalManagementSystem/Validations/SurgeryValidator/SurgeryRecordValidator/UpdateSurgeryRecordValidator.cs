using FluentValidation;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;

namespace HospitalManagementSystem.Validations.SurgeryValidator.SurgeryRecordValidator
{
    public class UpdateSurgeryRecordValidator : AbstractValidator<UpdateSurgeryRecordDto>
    {
        public UpdateSurgeryRecordValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("يجب تحديد المريض.");
            RuleFor(x => x.LeadSurgeonId).GreaterThan(0).WithMessage("يجب تحديد الجراح الأساسي.");
            RuleFor(x => x.OperatingRoomId).GreaterThan(0).WithMessage("يجب تحديد غرفة العمليات.");
            RuleFor(x => x.SurgeryType).NotEmpty().WithMessage("نوع العملية مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة العملية غير صالحة.");
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("وقت النهاية يجب أن يكون بعد وقت البداية.");
        }
    }
}
