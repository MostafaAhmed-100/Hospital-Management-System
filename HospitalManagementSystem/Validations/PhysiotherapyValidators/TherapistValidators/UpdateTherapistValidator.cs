using FluentValidation;
using HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs;

namespace HospitalManagementSystem.Validations.PhysiotherapyValidators.TherapistValidators
{
    public class UpdateTherapistValidator : AbstractValidator<UpdateTherapistDto>
    {
        public UpdateTherapistValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("اسم المعالج مطلوب.");
            RuleFor(x => x.Specialization).NotEmpty().WithMessage("التخصص مطلوب.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("يجب تحديد القسم.");
        }
    }
}
