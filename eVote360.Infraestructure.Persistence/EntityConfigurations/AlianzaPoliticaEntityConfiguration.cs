using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class AlianzaPoliticaEntityConfiguration : IEntityTypeConfiguration<AlianzaPolitica>
    {
        public void Configure(EntityTypeBuilder<AlianzaPolitica> builder)
        {
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("AlianzasPoliticas");
            #endregion

            #region Property configurations
            #endregion

            #region relationships
            builder.HasOne(a => a.PartidoSolicitante)
                .WithMany()
                .HasForeignKey(a => a.PartidoSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PartidoReceptor)
                .WithMany()
                .HasForeignKey(a => a.PartidoReceptorId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
