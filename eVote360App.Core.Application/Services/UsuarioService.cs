using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;

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

        public Task<bool> GetNombreUsuarioAsync(string nombreUsuario, int currentId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto?> LoginAsync(LoginRequestDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}

