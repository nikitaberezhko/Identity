using FluentValidation;
using Services.Services.Models.User.Request;

namespace Services.Validators.User;

public class AuthenticateValidator : AbstractValidator<AuthenticateUserModel>
{
    public AuthenticateValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        
        RuleFor(x => x.Password).NotEmpty();
    }
}