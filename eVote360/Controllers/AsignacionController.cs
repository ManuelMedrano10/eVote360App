using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AsignacionController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPartidoPoliticoService _partidoPoliticoService;
        private readonly IMapper _mapper;
        public AsignacionController(IUsuarioService usuarioService, IPartidoPoliticoService partidoPoliticoService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _partidoPoliticoService = partidoPoliticoService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _usuarioService.GetAllDirigentesAsync();
            var vms = _mapper.Map<List<UsuarioViewModel>>(dtos);

            return View(vms);
        }

        public async Task<IActionResult> Asignar(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) 
            {
                return RedirectToRoute(new { controller = "Asignacion", action = "Index" });
            } 

            var partidosDto = await _partidoPoliticoService.GetAllViewModelAsync();

            SaveAsignacionViewModel vm = new()
            {
                UsuarioId = id,
                NombreDirigente = $"{usuario.Nombre} {usuario.Apellido} (@{usuario.NombreUsuario})",
                PartidoPoliticoId = usuario.PartidoPoliticoId ?? 0,
                Partidos = _mapper.Map<List<PartidoPoliticoViewModel>>(partidosDto)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(SaveAsignacionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var partidosDto = await _partidoPoliticoService.GetAllViewModelAsync();
                vm.Partidos = _mapper.Map<List<PartidoPoliticoViewModel>>(partidosDto);
                return View(vm);
            }

            UpdateAsignacionDto dto = _mapper.Map<UpdateAsignacionDto>(vm);
            await _usuarioService.UpdateAsignacionAsync(dto);

            return RedirectToRoute(new { controller = "Asignacion", action = "Index" });
        }

        public async Task<IActionResult> Desvincular(int id)
        {
            await _usuarioService.RemoverAsignacionAsync(id);
            return RedirectToRoute(new { controller = "Asignacion", action = "Index" });
        }
    }
}
