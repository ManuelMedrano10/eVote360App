using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Common.Enums;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class UsuarioService : GenericService<SaveUsuarioDto, UsuarioDto, Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
            : base(usuarioRepository, mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> GetAllDirigentesAsync()
        {
            var dirigentes = await _usuarioRepository.GetAllQuery()
                .Include(u => u.PartidoPolitico)
                .Where(u => u.Rol == RolUsuario.DirigentePolitico && u.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<UsuarioDto>>(dirigentes);
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

        public async Task RemoverAsignacionAsync(int usuarioId)
        {
            var user = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (user != null)
            {
                user.PartidoPoliticoId = null;
                await _usuarioRepository.UpdateAsync(user.Id, user);
            }
        }

        public async Task UpdateAsignacionAsync(UpdateAsignacionDto dto)
        {
            var user = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
            if (user != null)
            {
                user.PartidoPoliticoId = dto.PartidoPoliticoId;
                await _usuarioRepository.UpdateAsync(user.Id, user);
            }
        }
    }
}

