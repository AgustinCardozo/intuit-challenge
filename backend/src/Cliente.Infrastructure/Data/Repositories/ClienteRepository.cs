using Cliente.Domain.Contracts.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Entities = Cliente.Domain.Entities;

namespace Cliente.Infrastructure.Data.Repositories
{
    public class ClienteRepository(ClienteDbContext clienteDbContext, ClienteDapperContext clienteDapperContext) : IClienteRepository
    {
        public async Task AddAsync(Entities.Cliente cliente)
        {
            using var connection = clienteDapperContext.CreateConnection();
            await connection.ExecuteScalarAsync(
                sql: "INSERT INTO intuit_cliente_db.public.clientes (nombre, apellido, razon_social, cuit, fecha_nacimiento, telefono_celular, email, fecha_creacion, fecha_modificacion) VALUES " +
                    "(@nombre, @apellido, @razon_social, @cuit, @fecha_nacimiento, @telefono_celular, @email, @fecha_creacion, @fecha_modificacion)",
                param: new
                {
                    nombre = cliente.Nombre,
                    apellido = cliente.Apellido,
                    razon_social = cliente.RazonSocial,
                    cuit = cliente.Cuit,
                    fecha_nacimiento = cliente.FechaNacimiento,
                    telefono_celular = cliente.TelefonoCelular,
                    email = cliente.Email,
                    fecha_creacion = cliente.FechaCreacion,
                    fecha_modificacion = cliente.FechaModificacion
                }
            );
        }

        public async Task DeleteAsync(Entities.Cliente cliente)
        {
            clienteDbContext.Remove(cliente);
            await clienteDbContext.SaveChangesAsync();
        }

        public async Task<List<Entities.Cliente>> GetAllAsync()
        {
            return await clienteDbContext.Clientes.ToListAsync();
        }

        public async Task<Entities.Cliente?> GetByIdAsync(int id)
        {
            return await clienteDbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Entities.Cliente>> SearchByNameAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return new List<Entities.Cliente>();
            }

            var normalizedSearch = searchString.Trim().ToLower();

            var clientes = await clienteDbContext.Clientes
                .Where(c => c.Nombre.ToLower().Contains(normalizedSearch) ||
                            c.Apellido.ToLower().Contains(normalizedSearch) ||
                            c.RazonSocial.ToLower().Contains(normalizedSearch))
                .ToListAsync();

            return clientes;
        }

        public async Task UpdateAsync(Entities.Cliente cliente)
        {
            var sqlCommand = @"
            CALL intuit_cliente_db.public.actualizar_cliente(
                p_id := @id, 
                p_nombre := @nombre, 
                p_apellido := @apellido, 
                p_telefono_celular := @telefono_celular, 
                p_email := @email
            );";
            using var connection = clienteDapperContext.CreateConnection();
            await connection.ExecuteScalarAsync(
                sql: sqlCommand,
                param: new 
                { 
                    id = cliente.Id,
                    nombre = cliente.Nombre,
                    apellido = cliente.Apellido,
                    telefono_celular = cliente.TelefonoCelular,
                    email = cliente.Email
                },
                commandType: CommandType.Text
            );
        }
    }
}
