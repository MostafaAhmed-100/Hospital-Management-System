using FluentValidation;
using HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.AppointmentDTOs;

namespace HospitalManagementSystem.Validations.OutpatientVisitsValidations.AppointmentValidations
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(x => x.ClinicId).GreaterThan(0).WithMessage("رقم العيادة غير صحيح.");
            RuleFor(x => x.DoctorId).GreaterThan(0).WithMessage("رقم الطبيب غير صحيح.");
            RuleFor(x => x.PatientId).GreaterThan(0).WithMessage("رقم المريض غير صحيح.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("تاريخ ووقت الحجز مطلوب.")
                .GreaterThan(DateTime.UtcNow).WithMessage("تاريخ الحجز يجب أن يكون في المستقبل.");
        }
    }
}