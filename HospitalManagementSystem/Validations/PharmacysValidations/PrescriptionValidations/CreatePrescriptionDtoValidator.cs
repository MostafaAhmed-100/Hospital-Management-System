using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.PrescriptionValidations
{
    public class CreatePrescriptionDtoValidator : AbstractValidator<CreatePrescriptionDto>
    {
        public CreatePrescriptionDtoValidator()
        {
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("رقم السجل الطبي غير صحيح.");
            RuleFor(x => x.DoctorId).GreaterThan(0).WithMessage("رقم الطبيب غير صحيح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");
        }
    }
}