using FluentValidation;
using HospitalManagementSystem.DTOs.InpatientDTOs.RoomDTOs;

namespace HospitalManagementSystem.Validations.InpatientDTOs.RoomDTOs
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم التعريف غير صالح.");
            RuleFor(x => x.RoomNumber).NotEmpty().WithMessage("رقم الغرفة مطلوب.");
            RuleFor(x => x.RoomType).IsInEnum().WithMessage("نوع الغرفة غير صالح.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("يجب تحديد القسم.");
        }
    }
}
