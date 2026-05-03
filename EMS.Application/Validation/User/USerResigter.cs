using EMS.Application.DTO.Empolyee;
using EMS.Application.DTO.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Validation.Employee
{
    public class USerResigter : AbstractValidator<RegisterDTO>
    {
        public USerResigter()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required")
                .GreaterThan(0).WithMessage("EmployeeId must be valid");    

        }
    }
}
