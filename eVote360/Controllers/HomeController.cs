using System.Security.Claims;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Ciudadano;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICiudadanoService _ciudadanoService;
        private readonly IEleccionService _eleccionService;
        private readonly IRegistroVotanteService _registroService;

        public HomeController(ICiudadanoService ciudadanoService, IEleccionService eleccionService, IRegistroVotanteService registroService)
        {
            _ciudadanoService = ciudadanoService;
            _eleccionService = eleccionService;
            _registroService = registroService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Administrador"))
                {
                    return RedirectToRoute(new { controller="Eleccion", action="Index"  });
                }
                if (User.IsInRole("DirigentePolitico"))
                {
                    return RedirectToRoute(new { controller = "Candidato", action = "Index" });
                }
                if (User.IsInRole("Elector"))
                {
                    return RedirectToRoute(new { controller = "Votacion", action = "Index" });
                }
            }

            bool hayEleccionActiva = await _eleccionService.GetEleccionActivaAsync();
            ViewBag.HayEleccion = hayEleccionActiva;

            return View(new VerifyCiudadanoViewModel() { DocumentoIdentidad = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Index(VerifyCiudadanoViewModel vm)
        {
            bool hayEleccionActiva = await _eleccionService.GetEleccionActivaAsync();
            ViewBag.HayEleccion = hayEleccionActiva;

            if (!hayEleccionActiva)
            {
                ModelState.AddModelError("", "No hay ningún proceso electoral en curso en este momento.");
                return View(vm);
            }

            if (!ModelState.IsValid) return View(vm);

            var ciudadano = await _ciudadanoService.GetByDocumentoAsync(vm.DocumentoIdentidad);
            if (ciudadano == null)
            {
                ModelState.AddModelError("DocumentoIdentidad", "Documento de identidad no encontrado o inactivo en el padrón electoral.");
                return View(vm);
            }
            var elecciones = await _eleccionService.GetAllViewModelAsync();
            var eleccionActiva = elecciones.FirstOrDefault(e => e.IsActive && !e.Finalizada);

            bool yaVoto = await _registroService.HaVotadoAsync(ciudadano.Id, eleccionActiva!.Id);
            if (yaVoto)
            {
                ModelState.AddModelError("", "El sistema registra que usted ya emitió su voto en este proceso electoral. No puede votar dos veces.");
                return View(vm);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, ciudadano.Id.ToString()),
                new (ClaimTypes.Name, ciudadano.DocumentoIdentidad),
                new ("NombreCompleto", $"{ciudadano.Nombre} {ciudadano.Apellido}"),
                new ("EleccionActivaId", eleccionActiva.Id.ToString()),
                new (ClaimTypes.Role, "Elector")
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToRoute(new { controller = "Votacion", action = "Index" });
        }

        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
