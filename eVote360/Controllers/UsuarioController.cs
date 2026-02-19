using System.Security.Claims;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }

            return View("Login", new LoginViewModel() { NombreUsuario = "", Password = ""});
        }

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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToRoute(new { controller = "Usuario", action = "Login" });
        }
    }
}
