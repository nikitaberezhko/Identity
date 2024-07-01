using FluentValidation;
using Services.Services.Models.User.Request;

namespace Services.Validators.User;

public class AuthorizationValidator : AbstractValidator<AuthorizationUserModel>
{
    public AuthorizationValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}