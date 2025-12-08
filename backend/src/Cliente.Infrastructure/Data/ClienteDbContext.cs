using Entities = Cliente.Domain.Entities;
using Cliente.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infrastructure.Data;

public partial class ClienteDbContext : DbContext
{
    public ClienteDbContext()
    {
    }

    public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entities.Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
    }
}
