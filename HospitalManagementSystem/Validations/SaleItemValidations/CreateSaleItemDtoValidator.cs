using FluentValidation;
using HospitalManagementSystem.DTOs.SaleItemDTOs;

namespace HospitalManagementSystem.Validations.SaleItemValidations
{
    public class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
    {
        public CreateSaleItemDtoValidator()
        {
            RuleFor(x => x.SaleId).GreaterThan(0).WithMessage("رقم الفاتورة غير صحيح.");
            RuleFor(x => x.MedicineId).GreaterThan(0).WithMessage("رقم الدواء غير صحيح.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من صفر.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الوحدة لا يمكن أن يكون بالسالب.");
        }
    }
}