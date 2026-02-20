using eVote360App.Core.Application.Dtos.Votos;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IVotoService
    {
        Task EmitirVotosAsync(List<SaveVotoDto> votos);
    }
}
