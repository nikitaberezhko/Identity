using FluentValidation;
using Services.Services.Models.User.Request;

namespace Services.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Login).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}