using FluentValidation;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;

namespace HospitalManagementSystem.Validations.SurgeryValidator.OperatingRoomValidator
{
    public class UpdateOperatingRoomValidator : AbstractValidator<UpdateOperatingRoomDto>
    {
        public UpdateOperatingRoomValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.RoomNumber).NotEmpty().WithMessage("رقم غرفة العمليات مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة الغرفة غير صالحة.");
        }
    }
}
