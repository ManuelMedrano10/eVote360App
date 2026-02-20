using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360App.Infraestructure.Persistence.EntityConfigurations
{
    public class VotoEntityConfiguration : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Votos");

            builder.HasOne(x => x.Eleccion)
                .WithMany()
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PuestoElectivo)
                .WithMany()
                .HasForeignKey(x => x.PuestoElectivoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Candidato)
                .WithMany()
                .HasForeignKey(x => x.CandidatoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
