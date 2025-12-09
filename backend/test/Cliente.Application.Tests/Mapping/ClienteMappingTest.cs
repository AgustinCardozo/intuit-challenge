using Cliente.Application.DTOs;
using Cliente.Application.Mapping;
using Entities = Cliente.Domain.Entities;

namespace Cliente.Application.Tests.Mapping
{
    public class ClienteMappingTest
    {
        [Fact]
        public void MappingClientToClientDto_Success()
        {
            var cliente = new Entities.Cliente
            {
                Id = 1,
                Nombre = "Test 1",
                Apellido = "Test 1",
                RazonSocial = "Test 1",
                Cuit = "27-12345678-1",
                TelefonoCelular = "1136923613",
                Email = "test1@mail.com",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
            };

            var clienteDto = ClienteMapping.MapToDto(cliente);

            Assert.NotNull(clienteDto);
            Assert.IsType<GetClienteDto>(clienteDto);
            Assert.IsNotType<Entities.Cliente>(clienteDto);
        }
    }
}
