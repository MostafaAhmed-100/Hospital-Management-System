using FluentValidation;
using HospitalManagementSystem.DTOs.DepartmentDTOs;

namespace HospitalManagementSystem.Validations.DepartmentValidations
{
    public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم القسم مطلوب ولا يمكن أن يكون فارغاً.")
                .MaximumLength(100).WithMessage("اسم القسم يجب ألا يتجاوز 100 حرف.")
                .MinimumLength(2).WithMessage("اسم القسم يجب أن يحتوي على حرفين على الأقل.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("الوصف يجب ألا يتجاوز 500 حرف.");
        }
    }
}