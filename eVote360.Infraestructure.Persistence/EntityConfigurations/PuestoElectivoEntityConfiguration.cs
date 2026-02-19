using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class PuestoElectivoEntityConfiguration : IEntityTypeConfiguration<PuestoElectivo>
    {
        public void Configure(EntityTypeBuilder<PuestoElectivo> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("PuestosElectivos");
            #endregion

            #region Property configurations
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Descripcion).IsRequired().HasMaxLength(255);
            #endregion

            #region relationships
            builder.HasMany(p => p.Candidatos)
                .WithOne(c => c.PuestoElectivo)
                .HasForeignKey(c => c.PuestoElectivoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
