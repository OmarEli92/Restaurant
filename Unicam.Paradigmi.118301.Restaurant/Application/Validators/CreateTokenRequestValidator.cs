using Application.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateTokenRequestValidator: AbstractValidator<CreateTokenRequest>
{
        public CreateTokenRequestValidator()
        {
            RuleFor(r => r.User.Email)
                .NotEmpty()
                .WithMessage("The email field can't be empty")
                .NotNull()
                .WithMessage("The email field can't be null")
                .MaximumLength(40);

            RuleFor(r => r.User.FirstName)
                .NotEmpty()
                .WithMessage("The first name field can't be empty")
                .NotNull()
                .WithMessage("The first name field can't be null")
                .MaximumLength(20);

            RuleFor(r => r.User.LastName)
                .NotEmpty()
                .WithMessage("The last name field can't be empty")
                .NotNull()
                .WithMessage("The last name field can't be null")
                .MaximumLength(20);

            RuleFor(r => r.User.Role)
               .Custom((r, ctx) => {
                   if (!(r.Equals("Customer")) && (r.Equals("Admin")))
                       ctx.AddFailure("The roles possible are Customer and Admin only");
                })
               .MaximumLength(20);

        }
}
}
