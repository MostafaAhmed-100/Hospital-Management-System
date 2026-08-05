using FluentValidation;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs;

namespace HospitalManagementSystem.Validations.SurgeryValidator.SurgeryRecordValidator
{
    public class CreateSurgeryRecordValidator : AbstractValidator<CreateSurgeryRecordDto>
    {
        public CreateSurgeryRecordValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("يجب تحديد المريض.");
            RuleFor(x => x.LeadSurgeonId).GreaterThan(0).WithMessage("يجب تحديد الجراح الأساسي.");
            RuleFor(x => x.OperatingRoomId).GreaterThan(0).WithMessage("يجب تحديد غرفة العمليات.");
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("يجب تحديد السجل الطبي.");
            RuleFor(x => x.SurgeryType).NotEmpty().WithMessage("نوع العملية مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة العملية غير صالحة.");
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("وقت النهاية يجب أن يكون بعد وقت البداية.");
        }
    }
}
