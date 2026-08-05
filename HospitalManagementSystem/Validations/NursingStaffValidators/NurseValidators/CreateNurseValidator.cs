using FluentValidation;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;

namespace HospitalManagementSystem.Validations.NursingStaffValidators.NurseValidators
{
    public class CreateNurseValidator : AbstractValidator<CreateNurseDto>
    {
        public CreateNurseValidator()
        {
            RuleFor(x => x.StaffId).GreaterThan(0).WithMessage("يجب تحديد الموظف.");
            RuleFor(x => x.LicenseNumber).NotEmpty().WithMessage("رقم الرخصة مطلوب.");
            RuleFor(x => x.Shift).IsInEnum().WithMessage("الشفت غير صالح.");
            RuleFor(x => x.WardSpecialization).NotEmpty().WithMessage("تخصص الجناح/العنبر مطلوب.");
        }
    }
}
