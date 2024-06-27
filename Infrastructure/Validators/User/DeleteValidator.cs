using FluentValidation;
using Services.Services.Contracts.User;

namespace WebApi.Models.User.Requests.Validators;

public class DeleteValidator : AbstractValidator<DeleteUserDto>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}