using Cliente.Application.DTOs;

namespace Cliente.Application.UseCases.Contracts
{
    public interface IClienteUseCase
    {
        Task AddAsync(AddClienteDto clienteDto);
        Task<bool> DeleteAsync(int id);
        Task<List<GetClienteDto>> GetAllAsync();
        Task<GetClienteDto?> GetByIdAsync(int id);
        Task<List<GetClienteDto>> SearchAsync(string searchName);
        Task<bool> UpdateAsync(int id, UpdateClienteDto clienteDto);
    }
}
