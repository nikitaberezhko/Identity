using FluentValidation;
using Services.Services.Contracts.User;

namespace Services.Validators.User;

public class AuthenticateValidator : AbstractValidator<AuthenticateUserDto>
{
    public AuthenticateValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        
        RuleFor(x => x.Password).NotEmpty();
    }
}