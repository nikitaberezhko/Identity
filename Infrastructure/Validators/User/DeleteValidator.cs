using FluentValidation;
using Services.Services.Contracts.User;

namespace Infrastructure.Validators.User;

public class DeleteValidator : AbstractValidator<DeleteUserDto>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}