using FluentValidation;
using TaskManagementAPI.Models;

public class DtoUserValidator : AbstractValidator<DtoUser>
{
    public DtoUserValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(user => user.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(role => role == "Admin" || role == "User").WithMessage("Role must be either 'Admin' or 'User'.");
    }
}
