using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Application.Dtos.Alianzas;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IAlianzaPoliticaService : IGenericService<SaveAlianzaPoliticaDto, AlianzaPoliticaDto, AlianzaPolitica>
    {
        Task<List<AlianzaPoliticaDto>> GetAllByPartidoIdAsync(int partidoId);
        Task<bool> GetAlianzaAsync(int partido1, int partido2, int currentId = 0);
    }
}
