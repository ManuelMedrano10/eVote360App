using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class PartidoPoliticoEntityConfiguration : IEntityTypeConfiguration<PartidoPolitico>
    {
        public void Configure(EntityTypeBuilder<PartidoPolitico> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("PartidosPoliticos");
            builder.HasIndex(p => p.Siglas).IsUnique();
            #endregion

            #region Property configurations
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Siglas).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Logo).IsRequired();
            #endregion

            #region relationships
            builder.HasMany(p => p.Candidatos)
                .WithOne(c => c.PartidoPolitico)
                .HasForeignKey(c => c.PartidoPoliticoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Dirigentes)
                .WithOne(d => d.PartidoPolitico)
                .HasForeignKey(d => d.PartidoPoliticoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
