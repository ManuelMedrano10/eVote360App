using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class UsuarioService : GenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
            : base(usuarioRepository, mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<bool> GetNombreUsuarioAsync(string nombreUsuario, int currentId = 0)
        {
            var query = _usuarioRepository.GetAllQuery()
                .Where(u => u.NombreUsuario == nombreUsuario && u.IsActive == true);

            if (currentId != 0)
            {
                query = query.Where(u => u.Id != currentId);
            }

            return await query.AnyAsync();
        }

        public async Task<UsuarioDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var usuario = await _usuarioRepository.GetAllQuery()
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginDto.NombreUsuario
                                       && u.Password == loginDto.Password
                                       && u.IsActive == true);

            if (usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}

