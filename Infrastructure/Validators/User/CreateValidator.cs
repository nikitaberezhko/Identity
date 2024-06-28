using FluentValidation;
using Services.Services.Contracts.User;

namespace WebApi.Models.User.Requests.Validators;

public class CreateValidator : AbstractValidator<CreateUserDto>
{
    public CreateValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Login).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}