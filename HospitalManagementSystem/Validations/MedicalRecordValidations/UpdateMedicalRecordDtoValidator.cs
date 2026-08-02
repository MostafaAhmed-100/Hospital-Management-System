using FluentValidation;
using HospitalManagementSystem.DTOs.MedicalRecordDTOs;

namespace HospitalManagementSystem.Validations.MedicalRecordValidations
{
    public class UpdateMedicalRecordDtoValidator : AbstractValidator<UpdateMedicalRecordDto>
    {
        public UpdateMedicalRecordDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم السجل الطبي غير صحيح.");

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("التشخيص الطبي مطلوب.")
                .MaximumLength(1000).WithMessage("التشخيص يجب ألا يتجاوز 1000 حرف.");
        }
    }
}