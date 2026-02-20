using eVote360App.Core.Application.Dtos.RegistroVotante;
using eVote360App.Core.Application.Dtos.Votos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Votacion;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Elector")]
    public class VotacionController : Controller
    {
        private readonly IPuestoElectivoService _puestoService;
        private readonly ICandidatoService _candidatoService;
        private readonly IVotoService _votoService;
        private readonly IRegistroVotanteService _registroService;
        public VotacionController(IPuestoElectivoService puestoService, ICandidatoService candidatoService, IVotoService votoService, IRegistroVotanteService registroService)
        {
            _puestoService = puestoService;
            _candidatoService = candidatoService;
            _votoService = votoService;
            _registroService = registroService;
        }
        public async Task<IActionResult> Index()
        {
            var puestos = await _puestoService.GetAllViewModelAsync();
            var candidatos = await _candidatoService.GetAllActivosParaVotacionAsync();

            PapeletaViewModel papeleta = new();

            foreach (var puesto in puestos.Where(p => p.IsActive))
            {
                var puestoVM = new PuestoPapeletaViewModel
                {
                    PuestoId = puesto.Id,
                    PuestoNombre = puesto.Nombre,
                    SeleccionCandidatoId = 0
                };

                var candidatosDelPuesto = candidatos.Where(c => c.PuestoElectivoId == puesto.Id).ToList();
                foreach (var cand in candidatosDelPuesto)
                {
                    puestoVM.Candidatos.Add(new CandidatoPapeletaViewModel
                    {
                        Id = cand.Id,
                        NombreCompleto = $"{cand.Nombre} {cand.Apellido}",
                        Foto = cand.Foto,
                        PartidoNombre = cand.PartidoNombre ?? "Independiente",
                        PartidoLogo = cand.PartidoLogo ?? ""
                    });
                }

                papeleta.Puestos.Add(puestoVM);
            }

            return View(papeleta);
        }

        [HttpPost]
        public async Task<IActionResult> EmitirVoto(PapeletaViewModel papeleta)
        {
            int ciudadanoId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            int eleccionId = int.Parse(User.FindFirst("EleccionActivaId")!.Value);

            bool yaVoto = await _registroService.HaVotadoAsync(ciudadanoId, eleccionId);
            if (yaVoto)
            {
                await HttpContext.SignOutAsync("CookieAuth");
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            List<SaveVotoDto> votosEmitidos = new();

            if (papeleta.Puestos != null)
            {
                foreach (var puesto in papeleta.Puestos)
                {
                    if (puesto.SeleccionCandidatoId != 0)
                    {
                        votosEmitidos.Add(new SaveVotoDto
                        {
                            EleccionId = eleccionId,
                            PuestoElectivoId = puesto.PuestoId,
                            CandidatoId = puesto.SeleccionCandidatoId
                        });
                    }
                }
            }
            if (votosEmitidos.Count != 0)
            {
                await _votoService.EmitirVotosAsync(votosEmitidos);
            }

            await _registroService.RegistrarVotoAsync(new SaveRegistroVotanteDto
            {
                CiudadanoId = ciudadanoId,
                EleccionId = eleccionId
            });
            await HttpContext.SignOutAsync("CookieAuth");

            return RedirectToRoute(new { controller = "Votacion", action = "Completado" });
        }

        [AllowAnonymous]
        public IActionResult Completado()
        {
            return View();
        }
    }
}
