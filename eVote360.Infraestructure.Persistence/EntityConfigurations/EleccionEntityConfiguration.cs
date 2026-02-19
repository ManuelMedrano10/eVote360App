using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class EleccionEntityConfiguration : IEntityTypeConfiguration<Eleccion>
    {
        public void Configure(EntityTypeBuilder<Eleccion> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Elecciones");
            #endregion

            #region Property configurations
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Fecha).IsRequired().HasMaxLength(100);
            #endregion

            #region relationships
            #endregion
        }
    }
}
