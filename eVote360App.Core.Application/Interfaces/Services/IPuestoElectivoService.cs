using eVote360App.Core.Application.Dtos.PuestoElectivo;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IPuestoElectivoService : IGenericService<SavePuestoElectivoDto, PuestoElectivoDto, PuestoElectivo>
    {
    }
}
