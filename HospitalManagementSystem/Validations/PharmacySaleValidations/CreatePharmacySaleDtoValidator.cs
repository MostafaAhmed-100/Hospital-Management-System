using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacySaleDTOs;

namespace HospitalManagementSystem.Validations.PharmacySaleValidations
{
    public class CreatePharmacySaleDtoValidator : AbstractValidator<CreatePharmacySaleDto>
    {
        public CreatePharmacySaleDtoValidator()
        {
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