using FluentValidation;
using HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs;

namespace HospitalManagementSystem.Validations.InpatientDTOs.BedDTOs
{
    public class UpdateBedValidator : AbstractValidator<UpdateBedDto>
    {
        public UpdateBedValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.BedNumber).NotEmpty().WithMessage("رقم السرير مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة السرير غير صالحة.");
            RuleFor(x => x.RoomId).GreaterThan(0).WithMessage("يجب تحديد الغرفة.");
        }
    }
}
