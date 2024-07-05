using FluentValidation;
using Services.Services.Models.User.Request;

namespace Services.Validators.User;

public class DeleteUserValidator : AbstractValidator<DeleteUserModel>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}