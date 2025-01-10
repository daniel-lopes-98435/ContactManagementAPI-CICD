using ContactManagement.Application.Dtos;
using FluentValidation;

namespace ContactManagement.Application.Validators
{
	public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(contact => contact.FirstName)
                .NotEmpty().WithMessage("O primeiro nome é obrigatório.")
                .MinimumLength(2).WithMessage("O primeiro nome deve ter pelo menos 2 caracteres.");

            RuleFor(contact => contact.LastName)
                .NotEmpty().WithMessage("O sobrenome é obrigatório.")
                .MinimumLength(2).WithMessage("O sobrenome deve ter pelo menos 2 caracteres.");

            RuleFor(contact => contact.AreaCode)
                .NotEmpty().WithMessage("O código de área é obrigatório.")
                .Matches(@"^\d{2,3}$").WithMessage("O código de área deve ter 2 ou 3 dígitos.");

            RuleFor(contact => contact.PhoneNumber)
                .NotEmpty().WithMessage("O número de telefone é obrigatório.")
                .Matches(@"^\d{8,11}$").WithMessage("O número de telefone deve conter entre 8 e 11 dígitos.");

            RuleFor(contact => contact.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");
        }
    
    }
}
