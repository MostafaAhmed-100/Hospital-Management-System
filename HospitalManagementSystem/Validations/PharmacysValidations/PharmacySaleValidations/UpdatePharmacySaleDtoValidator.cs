using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacySaleDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.PharmacySaleValidations
{
    public class UpdatePharmacySaleDtoValidator : AbstractValidator<UpdatePharmacySaleDto>
    {
        public UpdatePharmacySaleDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الفاتورة غير صحيح.");
            RuleFor(x => x.PharmacyId).GreaterThan(0).WithMessage("رقم الصيدلية غير صحيح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");

            RuleFor(x => x.PrescriptionId)
                .GreaterThan(0).When(x => x.PrescriptionId.HasValue)
                .WithMessage("رقم الروشتة غير صحيح.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("إجمالي الفاتورة لا يمكن أن يكون بالسالب.");
        }
    }
}