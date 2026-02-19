using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IUsuarioService : IGenericService<SaveUsuarioDto, UsuarioDto, Usuario> 
    {
        Task<UsuarioDto?> LoginAsync(LoginRequestDto loginDto);
        Task<bool> GetNombreUsuarioAsync(string nombreUsuario, int currentId = 0);
        Task<List<UsuarioDto>> GetAllDirigentesAsync();
        Task UpdateAsignacionAsync(UpdateAsignacionDto dto);
        Task RemoverAsignacionAsync(int usuarioId);
    }
}
