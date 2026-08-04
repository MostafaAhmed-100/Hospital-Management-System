using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.PharmacyInventoryDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.PharmacyInventoryValidations
{
    public class UpdatePharmacyInventoryDtoValidator : AbstractValidator<UpdatePharmacyInventoryDto>
    {
        public UpdatePharmacyInventoryDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الجرد غير صحيح.");
            RuleFor(x => x.PharmacyId).GreaterThan(0).WithMessage("رقم الصيدلية غير صحيح.");
            RuleFor(x => x.MedicineId).GreaterThan(0).WithMessage("رقم الدواء غير صحيح.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("الكمية لا يمكن أن تكون أقل من صفر.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("تاريخ الصلاحية يجب أن يكون في المستقبل.");
        }
    }
}