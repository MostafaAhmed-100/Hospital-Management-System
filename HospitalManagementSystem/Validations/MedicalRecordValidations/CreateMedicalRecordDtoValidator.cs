using FluentValidation;
using HospitalManagementSystem.DTOs.MedicalRecordDTOs;

namespace HospitalManagementSystem.Validations.MedicalRecordValidations
{
    public class CreateMedicalRecordDtoValidator : AbstractValidator<CreateMedicalRecordDto>
    {
        public CreateMedicalRecordDtoValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");
            RuleFor(x => x.DoctorId).GreaterThan(0).WithMessage("رقم الطبيب غير صحيح.");
            RuleFor(x => x.AppointmentId).GreaterThan(0).WithMessage("رقم الحجز غير صحيح.");

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("التشخيص الطبي مطلوب.")
                .MaximumLength(1000).WithMessage("التشخيص يجب ألا يتجاوز 1000 حرف.");
        }
    }
}