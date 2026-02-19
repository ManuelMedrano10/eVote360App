using eVote360App.Core.Application.Dtos.Ciudadanos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface ICiudadanoService : IGenericService<SaveCiudadanoDto, CiudadanoDto, Ciudadano>
    {
        Task<bool> GetDocumentoAsync(string documento, int currentId = 0);
    }
}
