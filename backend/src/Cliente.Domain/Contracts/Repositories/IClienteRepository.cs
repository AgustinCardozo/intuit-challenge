using Entities = Cliente.Domain.Entities;

namespace Cliente.Domain.Contracts.Repositories
{
    public interface IClienteRepository
    {
        Task AddAsync(Entities.Cliente cliente);
        Task DeleteAsync(Entities.Cliente cliente);
        Task<List<Entities.Cliente>> GetAllAsync();
        Task<Entities.Cliente?> GetByIdAsync(int id);
        Task<List<Entities.Cliente>> SearchByNameAsync(string searchString);
        Task UpdateAsync(Entities.Cliente cliente);
    }
}
