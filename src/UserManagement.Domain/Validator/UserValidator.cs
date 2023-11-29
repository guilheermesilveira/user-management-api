using FluentValidation;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio.")
            .Length(3, 50)
            .WithMessage("O nome deve conter entre {MinLength} e {MaxLength} caracteres.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email não pode ser vazio.")
            .EmailAddress()
            .WithMessage("O email fornecido não é válido.")
            .Length(3, 100)
            .WithMessage("O email deve conter entre {MinLength} e {MaxLength} caracteres.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A senha não pode ser vazia.")
            .Length(3, 250)
            .WithMessage("A senha deve conter entre {MinLength} e {MaxLength} caracteres.");
    }
}