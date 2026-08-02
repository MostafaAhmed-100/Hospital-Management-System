using FluentValidation;
using HospitalManagementSystem.DTOs.DoctorDTOs;

namespace HospitalManagementSystem.Validations.DoctorValidations
{
    public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("رقم الطبيب غير صحيح.");

            RuleFor(x => x.ConsultationFee)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الكشف لا يمكن أن يكون بالسالب.");

            RuleFor(x => x.DoctorType)
                .IsInEnum().WithMessage("نوع الطبيب غير صحيح.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("رقم القسم غير صحيح.");

            RuleFor(x => x.SpecialtyId)
                .GreaterThan(0).WithMessage("رقم التخصص غير صحيح.");
        }
    }
}