using EMS.Application.DTO.Empolyee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Validation.Employee
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeDTO>
    {
        public AddEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("RoleId must be valid");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("DepartmentId must be valid");
        }
    }
}
