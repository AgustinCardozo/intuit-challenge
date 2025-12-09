using Cliente.Application.UseCases;
using Cliente.Domain.Contracts.Repositories;
using Entities = Cliente.Domain.Entities;
using Moq;
using Cliente.Application.DTOs;
using Cliente.Domain.Exceptions;

namespace Cliente.Application.Tests.UseCases
{
    public class ClienteUseCaseTest
    {
        private readonly Mock<IClienteRepository> mockRepo;
        private readonly ClienteUseCase clienteUseCase;

        public ClienteUseCaseTest()
        {
            mockRepo = new Mock<IClienteRepository>();
            clienteUseCase = new ClienteUseCase(mockRepo.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            mockRepo.Setup(c => c.AddAsync(It.IsAny<Entities.Cliente>()));

            var clienteDto = new AddClienteDto()
            {
                Nombre = "Test",
                Apellido = "Test",
                RazonSocial = "Test",
                Celular = "1122223333",
                Email = "test@mail.com",
                Cuit = "20-12345678-1",
            }; 

            await clienteUseCase.AddAsync(clienteDto);

            mockRepo.Verify(c => c.AddAsync(It.IsAny<Entities.Cliente>()), Times.Once());
        }

        [Fact]
        public async Task AddAsync_Exception()
        {
            mockRepo.Setup(c => c.AddAsync(It.IsAny<Entities.Cliente>()));

            var clienteDto = new AddClienteDto()
            {
                Nombre = "",
                Apellido = null,
                RazonSocial = "Test",
                Celular = "1122223333",
                Email = "test@mail.com",
                Cuit = "20123456781",
            };

            await Assert.ThrowsAsync<ClientException>(async () => await clienteUseCase.AddAsync(clienteDto));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task DeleteAsync(int id)
        {
            mockRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetClients().Find(c => c.Id == id));
            mockRepo.Setup(c => c.DeleteAsync(It.IsAny<Entities.Cliente>()));
            
            var wasDeleted = await clienteUseCase.DeleteAsync(id);

            mockRepo.Verify(c => c.GetByIdAsync(It.IsAny<int>()), Times.Once());

            if (!wasDeleted)
            {
                Assert.False(wasDeleted);
                mockRepo.Verify(c => c.DeleteAsync(It.IsAny<Entities.Cliente>()), Times.Never());
                return;
            }

            Assert.True(wasDeleted);
            mockRepo.Verify(c => c.DeleteAsync(It.IsAny<Entities.Cliente>()), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_ReturnClients()
        {
            mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(GetClients());
            var clientes = await clienteUseCase.GetAllAsync();
            Assert.NotNull(clientes);
            Assert.Equal(2, clientes.Count);
        }

        [Fact]
        public async Task GetAllAsync_ReturnEmpty()
        {
            mockRepo.Setup(c => c.GetAllAsync());
            var clientes = await clienteUseCase.GetAllAsync();
            Assert.NotNull(clientes);
            Assert.Empty(clientes);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetByIdAsync(int id)
        {
            mockRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetClients().Find(c => c.Id == id)); 
            var cliente = await clienteUseCase.GetByIdAsync(id);
            
            if(id > 2)
            {
                Assert.Null(cliente);
                return; 
            }

            Assert.NotNull(cliente);
            Assert.Equal(1, cliente.Id);
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("prueba")]
        public async Task SearchAsync(string name)
        {
            mockRepo.Setup(c => c.SearchByNameAsync(It.IsAny<string>())).ReturnsAsync(GetClients().Where(c => c.Email.Contains(name)).ToList());
            var clientes = await clienteUseCase.SearchAsync(name);

            if (name.Contains("prueba"))
            {
                Assert.Empty(clientes);
                return;
            }

            Assert.NotNull(clientes);
            Assert.Contains("test1", clientes.First().Email);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task UpdateAsync(int id)
        {
            mockRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetClients().Find(c => c.Id == id));
            mockRepo.Setup(c => c.UpdateAsync(It.IsAny<Entities.Cliente>()));

            var clienteDto = new UpdateClienteDto()
            {
                Nombre = "Test",
                Apellido = "Test",
                Celular = "1122223333",
                Email = "test@mail.com"
            };

            var wasUpdated = await clienteUseCase.UpdateAsync(id, clienteDto);

            mockRepo.Verify(c => c.GetByIdAsync(It.IsAny<int>()), Times.Once());

            if (!wasUpdated) 
            {
                Assert.False(wasUpdated);
                return;
            }

            mockRepo.Verify(c => c.UpdateAsync(It.IsAny<Entities.Cliente>()), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Exception()
        {
            mockRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetClients().Find(c => c.Id == 1));
            mockRepo.Setup(c => c.UpdateAsync(It.IsAny<Entities.Cliente>()));

            var clienteDto = new UpdateClienteDto()
            {
                Nombre = "",
                Apellido = null,
                Celular = "1122223333",
                Email = "test",
            };

            await Assert.ThrowsAsync<ClientException>(async () => await clienteUseCase.UpdateAsync(1, clienteDto));
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
