using FluentValidation;
using HospitalManagementSystem.DTOs.InpatientDTOs.AdmissionDTOs;

namespace HospitalManagementSystem.Validations.InpatientValidations.AdmissionValidations
{
    public class CreateAdmissionValidator : AbstractValidator<CreateAdmissionDto>
    {
        public CreateAdmissionValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("يجب تحديد المريض.");
            RuleFor(x => x.DoctorId).GreaterThan(0).WithMessage("يجب تحديد الطبيب.");
            RuleFor(x => x.BedId).GreaterThan(0).WithMessage("يجب تحديد السرير.");
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("يجب ربط التنويم بسجل طبي.");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("سبب التنويم مطلوب.");
        }
    }
}
