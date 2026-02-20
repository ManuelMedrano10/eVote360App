using eVote360App.Core.Application.Dtos.Candidatos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface ICandidatoService : IGenericService<SaveCandidatoDto, CandidatoDto, Candidato>
    {
        Task<List<CandidatoDto>> GetAllByPartidoIdAsync(int partidoId);
        Task<List<CandidatoDto>> GetAllActivosParaVotacionAsync();
    }
}
