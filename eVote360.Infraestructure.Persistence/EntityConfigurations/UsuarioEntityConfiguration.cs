using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Usuarios");
            builder.HasIndex(u => u.NombreUsuario).IsUnique();
            #endregion

            #region Property configurations
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(40);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(40);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);
            #endregion

            #region relationships
            #endregion
        }
    }
}
