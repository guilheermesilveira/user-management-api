using FluentValidation.Results;
using UserManagement.Domain.Validator;

namespace UserManagement.Domain.Models;

public class User : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new UserValidator().Validate(this);
        return validationResult.IsValid;
    }
}