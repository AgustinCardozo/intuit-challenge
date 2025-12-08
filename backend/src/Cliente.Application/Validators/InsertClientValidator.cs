using Cliente.Application.DTOs;
using FluentValidation;

namespace Cliente.Application.Validators
{
    public class InsertClientValidator : AbstractValidator<AddClienteDto>
    {
        public InsertClientValidator()
        {
            RuleFor(req => req.Nombre).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(req => req.Apellido).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(req => req.RazonSocial).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(req => req.Cuit).NotNull().NotEmpty().MaximumLength(11);
            RuleFor(req => req.Celular).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(req => req.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
