using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IUsuarioService : IGenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario> 
    {
        Task<UsuarioDto?> LoginAsync(LoginRequestDto loginDto);
        Task<bool> GetNombreUsuarioAsync(string nombreUsuario, int currentId = 0);
    }
}
