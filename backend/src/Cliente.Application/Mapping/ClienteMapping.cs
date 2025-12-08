using Cliente.Application.DTOs;
using Entities = Cliente.Domain.Entities;

namespace Cliente.Application.Mapping
{
    public static class ClienteMapping
    {
        public static GetClienteDto MapToDto(Entities.Cliente cliente)
        {
            return new GetClienteDto()
            {
                Id = cliente!.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                RazonSocial = cliente.RazonSocial,
                Celular = cliente.TelefonoCelular,
                Cuit = cliente.Cuit,
                Email = cliente.Email,
                FechaDeNacimiento = cliente.FechaNacimiento
            };
        }
    }
}
