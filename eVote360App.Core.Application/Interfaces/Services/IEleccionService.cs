using eVote360App.Core.Application.Dtos.Elecciones;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IEleccionService : IGenericService<SaveEleccionDto, EleccionDto, Eleccion>
    {
        Task<bool> GetEleccionActivaAsync(int? currentEleccionId = null);
        Task FinalizarEleccionAsync(int id);
        Task<ResultadoEleccionDto?> GetResultadosAsync(int eleccionId);
    }
}
