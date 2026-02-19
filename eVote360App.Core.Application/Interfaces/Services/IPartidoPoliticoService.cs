using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Application.Dtos.PartidosPoliticos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IPartidoPoliticoService : IGenericService<SavePartidoPoliticoDto, PartidoPoliticoDto, PartidoPolitico>
    {
        Task<bool> GetSiglaAsync(string siglas, int currentId = 0);
    }
}
