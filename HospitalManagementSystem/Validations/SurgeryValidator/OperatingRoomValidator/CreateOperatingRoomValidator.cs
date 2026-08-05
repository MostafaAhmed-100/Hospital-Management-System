using FluentValidation;
using HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs;

namespace HospitalManagementSystem.Validations.SurgeryValidator.OperatingRoomValidator
{
    public class CreateOperatingRoomValidator : AbstractValidator<CreateOperatingRoomDto>
    {
        public CreateOperatingRoomValidator()
        {
            RuleFor(x => x.RoomNumber).NotEmpty().WithMessage("رقم غرفة العمليات مطلوب.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("حالة الغرفة غير صالحة.");
        }
    }
}
