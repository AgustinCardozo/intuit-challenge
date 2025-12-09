using Cliente.Infrastructure.Data;
using Cliente.Infrastructure.Data.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Dapper;
using Moq.EntityFrameworkCore;
using System.Data;
using Entities = Cliente.Domain.Entities;

namespace Cliente.Infrastructure.Tests.Data.Repositories
{
    public class ClienteRepositoryTest
    {
        private readonly Mock<ClienteDbContext> mockDbContext;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<IClienteDapperContext> mockDapperContext;
        private readonly ClienteDapperContext dapperContext;
        private readonly ClienteRepository repository;

        public ClienteRepositoryTest()
        {
            mockDbContext = new Mock<ClienteDbContext>();
            mockConfiguration = new Mock<IConfiguration>();
            mockDapperContext = new Mock<IClienteDapperContext>();
            dapperContext = new ClienteDapperContext(mockConfiguration.Object);
            repository = new ClienteRepository(mockDbContext.Object, dapperContext);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            mockDbContext.Setup(x => x.Clientes).ReturnsDbSet(GetClients());
            mockDbContext.Setup(m => m.Set<Entities.Cliente>()).Returns(mockDbContext.Object.Clientes);
            var newClient = new Entities.Cliente();

            await repository.DeleteAsync(newClient);

            mockDbContext.Verify(m => m.Remove(newClient), Times.Once());
            mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_ReturnClients()
        {
            mockDbContext.Setup(m => m.Set<Entities.Cliente>()).Returns(mockDbContext.Object.Clientes);
            mockDbContext.Setup(x => x.Clientes).ReturnsDbSet(GetClients());

            var clientsDb = await repository.GetAllAsync();

            Assert.NotNull(clientsDb);
            Assert.Equal(2, clientsDb.Count);
            Assert.DoesNotContain(clientsDb, i => i.Id == 3);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnPublication()
        {
            mockDbContext.Setup(x => x.Clientes).ReturnsDbSet(GetClients());
            mockDbContext.Setup(m => m.Set<Entities.Cliente>()).Returns(mockDbContext.Object.Clientes);

            var clientDb = await repository.GetByIdAsync(2);

            Assert.NotNull(clientDb);
            Assert.Equal(2, clientDb.Id);
        }

        private static List<Entities.Cliente> GetClients()
        {
            return new List<Entities.Cliente> {
                new Entities.Cliente
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
                },
                new Entities.Cliente
                {
                    Id = 2,
                    Nombre = "Test 2",
                    Apellido = "Test 2",
                    RazonSocial = "Test 2",
                    Cuit = "27-12345678-2",
                    TelefonoCelular = "1136923614",
                    Email = "test2@mail.com",
                    FechaNacimiento = DateTime.Now.AddYears(-30),
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                }
            };
        }
    }
}
