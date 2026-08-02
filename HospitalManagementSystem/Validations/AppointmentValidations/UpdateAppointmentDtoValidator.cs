using FluentValidation;
using HospitalManagementSystem.DTOs.AppointmentDTOs;

namespace HospitalManagementSystem.Validations.AppointmentValidations
{
    public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("رقم الحجز غير صحيح.");
            RuleFor(x => x.ClinicId).GreaterThan(0).WithMessage("رقم العيادة غير صحيح.");
            RuleFor(x => x.DoctorId).GreaterThan(0).WithMessage("رقم الطبيب غير صحيح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("تاريخ ووقت الحجز مطلوب.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("حالة الحجز غير صحيحة.");
        }
    }
}