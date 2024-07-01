using FluentValidation;
using Services.Services.Contracts.User;

namespace Services.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Login).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}