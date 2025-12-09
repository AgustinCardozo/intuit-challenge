using Cliente.Application.DTOs;
using Cliente.Application.Mapping;
using Cliente.Application.UseCases.Contracts;
using Cliente.Application.Validators;
using Cliente.Domain.Contracts.Repositories;
using Cliente.Domain.Exceptions;
using System.Text;
using Entities = Cliente.Domain.Entities;

namespace Cliente.Application.UseCases
{
    public class ClienteUseCase(IClienteRepository clienteRepository) : IClienteUseCase
    {
        public async Task AddAsync(AddClienteDto clienteDto)
        {
            var result = new InsertClientValidator().Validate(clienteDto);
            if (!result.IsValid)
            {
                var errors = result.Errors.ToList();
                var errorMsg = new StringBuilder();
                errorMsg.AppendLine("Datos de registros incorrectos: ");
                errors.ForEach(error => errorMsg.AppendLine($"\t{error}"));
                throw new ClientException(errorMsg.ToString());
            }

            var cliente = new Entities.Cliente()
            {
                Nombre = clienteDto.Nombre!,
                Apellido = clienteDto.Apellido!,
                RazonSocial = clienteDto.RazonSocial!,
                Cuit = clienteDto.Cuit!,
                Email = clienteDto.Email!,
                FechaNacimiento = clienteDto.FechaDeNacimiento,
                TelefonoCelular = clienteDto.Celular!,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
            }; 
            await clienteRepository.AddAsync(cliente); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);

            if (cliente == null)
            {
                return false;
            }

            await clienteRepository.DeleteAsync(cliente);
            return true;
        }

        public async Task<List<GetClienteDto>> GetAllAsync()
        {
            var clientes = await clienteRepository.GetAllAsync();

            if(clientes == null || clientes.Count == 0)
            {
                return new List<GetClienteDto>();
            }
            
            return clientes.Select(c => ClienteMapping.MapToDto(c)).ToList();
        }

        public async Task<GetClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);

            if (cliente == null)
            {
                return null;
            }

            return ClienteMapping.MapToDto(cliente);
        }

        public async Task<List<GetClienteDto>> SearchAsync(string searchName)
        {
            var clientes = await clienteRepository.SearchByNameAsync(searchName);

            if (clientes == null || clientes.Count == 0)
            {
                return new List<GetClienteDto>();
            }

            return clientes.Select(c => ClienteMapping.MapToDto(c)).ToList();
        }

        public async Task<bool> UpdateAsync(int id, UpdateClienteDto clienteDto)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);

            if (cliente == null)
            {
                return false;
            }

            var result = new UpdateClientValidator().Validate(clienteDto);
            if (!result.IsValid)
            {
                var errors = result.Errors.ToList();
                var errorMsg = new StringBuilder();
                errorMsg.AppendLine("Datos de registros incorrectos: ");
                errors.ForEach(error => errorMsg.AppendLine($"{error}"));
                throw new ClientException(errorMsg.ToString());
            }

            cliente.Nombre = clienteDto.Nombre!;
            cliente.Apellido = clienteDto.Apellido!;
            cliente.TelefonoCelular = clienteDto.Celular!;
            cliente.Email = clienteDto.Email!;

            await clienteRepository.UpdateAsync(cliente);
            return true;
        }
    }
}
