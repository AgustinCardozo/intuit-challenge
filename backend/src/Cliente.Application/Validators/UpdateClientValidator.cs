using Cliente.Application.DTOs;
using FluentValidation;

namespace Cliente.Application.Validators
{
    public class UpdateClientValidator : AbstractValidator<UpdateClienteDto>
    {
        public UpdateClientValidator()
        {
            RuleFor(req => req.Nombre).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(req => req.Apellido).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(req => req.Celular).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(req => req.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
