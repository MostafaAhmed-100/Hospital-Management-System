using FluentValidation;
using HospitalManagementSystem.DTOs.PrescriptionDTOs;

namespace HospitalManagementSystem.Validations.PrescriptionValidations
{
    public class UpdatePrescriptionDtoValidator : AbstractValidator<UpdatePrescriptionDto>
    {
        public UpdatePrescriptionDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الروشتة غير صحيح.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة الروشتة غير صحيحة.");
        }
    }
}