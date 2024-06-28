using FluentValidation;
using Services.Services.Contracts.User;

namespace Infrastructure.Validators;

public class AuthorizationValidator : AbstractValidator<AuthorizationUserDto>
{
    public AuthorizationValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}