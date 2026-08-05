using FluentValidation;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs;

namespace HospitalManagementSystem.Validations.SurgeryValidator.SurgeryTeamValidators
{
    public class CreateSurgeryTeamValidator : AbstractValidator<CreateSurgeryTeamDto>
    {
        public CreateSurgeryTeamValidator()
        {
            RuleFor(x => x.SurgeryId).GreaterThan(0).WithMessage("يجب تحديد العملية.");
            RuleFor(x => x.StaffId).GreaterThan(0).WithMessage("يجب تحديد الموظف/الطاقم.");
            RuleFor(x => x.RoleInSurgery).IsInEnum().WithMessage("دور الموظف في العملية غير صالح.");
        }
    }
}
