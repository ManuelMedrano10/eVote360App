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
    public class RegistroVotoEntityConfiguration : IEntityTypeConfiguration<RegistroVotante>
    {
        public void Configure(EntityTypeBuilder<RegistroVotante> builder)
        {
            builder.HasOne(x => x.Eleccion)
                .WithMany()
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Ciudadano)
                .WithMany()
                .HasForeignKey(x => x.CiudadanoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
