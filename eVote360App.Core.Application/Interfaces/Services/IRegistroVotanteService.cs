using eVote360App.Core.Application.Dtos.RegistroVotante;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IRegistroVotanteService
    {
        Task<bool> HaVotadoAsync(int ciudadanoId, int eleccionId);
        Task RegistrarVotoAsync(SaveRegistroVotanteDto dto);
    }
}
