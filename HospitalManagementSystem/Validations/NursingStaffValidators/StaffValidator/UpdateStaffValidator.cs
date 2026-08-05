using FluentValidation;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;

namespace HospitalManagementSystem.Validations.NursingStaffValidators.StaffValidator
{
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffDto>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("اسم الموظف مطلوب.").MaximumLength(150).WithMessage("الاسم لا يجب أن يتجاوز 150 حرف.");
            RuleFor(x => x.ClinicId).GreaterThan(0).WithMessage("يجب تحديد العيادة.");
            RuleFor(x => x.Role).IsInEnum().WithMessage("الدور الوظيفي غير صالح.");
        }
    }
}
