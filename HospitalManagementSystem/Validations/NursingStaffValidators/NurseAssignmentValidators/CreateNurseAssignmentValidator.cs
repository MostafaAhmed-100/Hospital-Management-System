using FluentValidation;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;

namespace HospitalManagementSystem.Validations.NursingStaffValidators.NurseAssignmentValidators
{
    public class CreateNurseAssignmentValidator : AbstractValidator<CreateNurseAssignmentDto>
    {
        public CreateNurseAssignmentValidator()
        {
            RuleFor(x => x.NurseId).GreaterThan(0).WithMessage("يجب تحديد الممرض.");
            RuleFor(x => x.Shift).IsInEnum().WithMessage("الشفت غير صالح.");

            RuleFor(x => x)
                .Must(x => x.AdmissionId.HasValue || x.ErVisitId.HasValue)
                .WithMessage("يجب ربط التكليف إما بحالة تنويم داخلي أو زيارة طوارئ.");
        }
    }
}
