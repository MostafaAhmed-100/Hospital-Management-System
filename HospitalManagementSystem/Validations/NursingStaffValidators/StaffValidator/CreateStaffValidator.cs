using FluentValidation;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;

namespace HospitalManagementSystem.Validations.NursingStaffValidators.StaffValidator
{
    public class CreateStaffValidator : AbstractValidator<CreateStaffDto>
    {
        public CreateStaffValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("اسم الموظف مطلوب.").MaximumLength(150).WithMessage("الاسم لا يجب أن يتجاوز 150 حرف.");
            RuleFor(x => x.ClinicId).GreaterThan(0).WithMessage("يجب تحديد العيادة.");
            RuleFor(x => x.Role).IsInEnum().WithMessage("الدور الوظيفي غير صالح.");
        }
    }
}
