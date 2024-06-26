﻿using FluentValidation;
using Lavyn.Domain.Dtos;

namespace Lavyn.Business.Validation.Validators
{
    class CredentialValidator : AbstractValidator<CredentialDto>
    {
        public CredentialValidator()
        {
            RuleFor(x => x.Login)
                .EmailAddress()
                .WithMessage("Invalid Login. Must be a valid e-mail");
            
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("The password cannot be null")
                .NotEmpty()
                .WithMessage("The password cannot be empty")
                .MinimumLength(6)
                .WithMessage("The password must have at least 6 characters");
        }
    }
}
