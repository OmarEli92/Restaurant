using Application.Extensions;
using Application.Models.Requests.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateUserRequestValidator: AbstractValidator<RegistrationRequest>
    {
        public CreateUserRequestValidator() {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("The email field can't be empty")
                .NotNull()
                .WithMessage("The email field can't be null")
                .MaximumLength(40);

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("The first name field can't be empty")
                .NotNull()
                .WithMessage("The first name field can't be null")
                .MaximumLength(20);

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("The last name field can't be empty")
                .NotNull()
                .WithMessage("The last name field can't be null")
                .MaximumLength(20);

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("The password field can't be empty")
                .NotNull()
                .WithMessage("The password field can't be null")
                .MinimumLength(8)
                .MaximumLength(20)
                .WithMessage("The password field can't be empty should be at least 8 characters long")
                .RegEx("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{6,}$"
                , "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale"
                );
               

        }
    }
}

