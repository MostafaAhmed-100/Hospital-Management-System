using FluentValidation;

namespace HospitalManagementSystem.DTOs.InpatientDTOs.BedDTOs
{
    public class CreateBedValidator : AbstractValidator<CreateBedDto>
    {
        public CreateBedValidator()
        {
            RuleFor(x => x.BedNumber).NotEmpty().WithMessage("رقم السرير مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة السرير غير صالحة.");
            RuleFor(x => x.RoomId).GreaterThan(0).WithMessage("يجب تحديد الغرفة.");
        }
    }
}