using FluentValidation;
using Services.Services.Contracts.User;

namespace Services.Validators.User;

public class DeleteUserValidator : AbstractValidator<DeleteUserDto>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}