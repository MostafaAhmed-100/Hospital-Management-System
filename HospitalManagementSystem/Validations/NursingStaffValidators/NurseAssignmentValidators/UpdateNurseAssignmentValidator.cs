using FluentValidation;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs;

namespace HospitalManagementSystem.Validations.NursingStaffValidators.NurseAssignmentValidators
{
    public class UpdateNurseAssignmentValidator : AbstractValidator<UpdateNurseAssignmentDto>
    {
        public UpdateNurseAssignmentValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.NurseId).GreaterThan(0).WithMessage("يجب تحديد الممرض.");
            RuleFor(x => x.Shift).IsInEnum().WithMessage("الشفت غير صالح.");
        }
    }
}
