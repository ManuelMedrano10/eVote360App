using System.Security.Claims;
using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var usuariosDto = await _usuarioService.GetAllViewModelAsync();
            var usuariosVm = _mapper.Map<List<UsuarioViewModel>>(usuariosDto);

            return View(usuariosVm);
        }

        [Authorize(Roles = "Administrador")]
        [BlockDuringElectionAtribute]
        public IActionResult Save()
        {
            return View(new SaveUsuarioViewModel() 
            { 
                Id = 0,
                Nombre = "",
                Apellido = "",
                Email = "",
                NombreUsuario = ""
            });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save(SaveUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Id == 0 && string.IsNullOrEmpty(vm.Password))
            {
                ModelState.AddModelError("Password", "La contraseña es obligatoria al crear un usuario.");
            }

            bool existeUsuario = await _usuarioService.GetNombreUsuarioAsync(vm.NombreUsuario, vm.Id);
            if (existeUsuario)
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya esta usado.");
                return View(vm);
            }

            SaveUsuarioDto dto = _mapper.Map<SaveUsuarioDto>(vm);

            if (vm.Id != 0 && string.IsNullOrEmpty(vm.Password))
            {
                var usuarioExistente = await _usuarioService.GetByIdAsync(vm.Id);
                dto.Password = usuarioExistente.Password;
            }

            if (vm.Id == 0)
            {
                await _usuarioService.AddAsync(dto);
            }
            else
            {
                await _usuarioService.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        [Authorize(Roles = "Administrador")]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioDto = await _usuarioService.GetByIdAsync(id);
            if (usuarioDto == null)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SaveUsuarioViewModel vm = _mapper.Map<SaveUsuarioViewModel>(usuarioDto);
            vm.Password = string.Empty;
            vm.ConfirmPassword = string.Empty;

            return View("Save", vm);
        }

        [Authorize(Roles = "Administrador")]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null && int.Parse(userIdClaim) == id)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            await _usuarioService.ChangeStatusAsync(id);
            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        #region Autenticacion de Usuarios
        [AllowAnonymous]
        public IActionResult Login()
        {
            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }

            return View("Login", new LoginViewModel() { NombreUsuario = "", Password = ""});
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            LoginRequestDto loginRequest = new()
            {
                NombreUsuario = vm.NombreUsuario,
                Password = vm.Password
            };

            UsuarioDto? usuario = await _usuarioService.LoginAsync(loginRequest);

            if(usuario == null)
            {
                ModelState.AddModelError("", "Datos ingresados invalidos o usuario incorrecto.");
                return View(vm);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new (ClaimTypes.Name, usuario.NombreUsuario),
                new (ClaimTypes.Email, usuario.Email),
                new ("NombreCompleto", $"{usuario.Nombre} {usuario.Apellido}"),
                new (ClaimTypes.Role, usuario.Rol == 1 ? "Administrador" : "DirigentePolitico")
            };

            if (usuario.PartidoPoliticoId.HasValue)
            {
                claims.Add(new ("PartidoPoliticoId", usuario.PartidoPoliticoId.Value.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToRoute(new { controller = "Usuario", action = "Login" });
        }
        #endregion
    }
}
