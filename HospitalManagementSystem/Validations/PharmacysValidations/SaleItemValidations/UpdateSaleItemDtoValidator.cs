using FluentValidation;
using HospitalManagementSystem.DTOs.PharmacysDTOS.SaleItemDTOs;

namespace HospitalManagementSystem.Validations.PharmacysValidations.SaleItemValidations
{
    public class UpdateSaleItemDtoValidator : AbstractValidator<UpdateSaleItemDto>
    {
        public UpdateSaleItemDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم العنصر غير صحيح.");
            RuleFor(x => x.SaleId).GreaterThan(0).WithMessage("رقم الفاتورة غير صحيح.");
            RuleFor(x => x.MedicineId).GreaterThan(0).WithMessage("رقم الدواء غير صحيح.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من صفر.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الوحدة لا يمكن أن يكون بالسالب.");
        }
    }
}