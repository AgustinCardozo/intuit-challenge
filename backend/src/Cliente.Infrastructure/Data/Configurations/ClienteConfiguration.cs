using Entities = Cliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cliente.Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Entities.Cliente>
    {
        public void Configure(EntityTypeBuilder<Entities.Cliente> builder)
        {
            builder.HasKey(e => e.Id).HasName("clientes_pkey");

            builder.ToTable("clientes");

            builder.HasIndex(e => e.Cuit, "clientes_cuit_key").IsUnique();
            builder.HasIndex(e => e.Email, "clientes_email_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            builder.Property(e => e.Cuit)
                .HasMaxLength(20)
                .HasColumnName("cuit");
            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            builder.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_modificacion");
            builder.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            builder.Property(e => e.RazonSocial)
                .HasMaxLength(150)
                .HasColumnName("razon_social");
            builder.Property(e => e.TelefonoCelular)
                .HasMaxLength(30)
                .HasColumnName("telefono_celular");
        }
    }
}
