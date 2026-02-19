using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class CiudadanoEntityConfiguration : IEntityTypeConfiguration<Ciudadano>
    {
        public void Configure(EntityTypeBuilder<Ciudadano> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Ciudadanos");
            builder.HasIndex(c => c.DocumentoIdentidad).IsUnique();
            #endregion

            #region Property configurations
            builder.Property(c => c.Nombre).IsRequired().HasMaxLength(40);
            builder.Property(c => c.Apellido).IsRequired().HasMaxLength(40);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
            builder.Property(c => c.DocumentoIdentidad).IsRequired().HasMaxLength(11);
            #endregion

            #region relationships
            #endregion
        }
    }
}
