using AutoMapper;
using eVote360App.Core.Application.Dtos.Alianzas;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Services;
using eVote360App.Core.Application.Viewmodels.Alianzas;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using eVote360App.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "DirigentePolitico")]
    public class AlianzaPoliticaController : Controller
    {
        private readonly IAlianzaPoliticaService _alianzaPoliticaService;
        private readonly IPartidoPoliticoService _partidoPoliticoService;
        private readonly IMapper _mapper;
        public AlianzaPoliticaController(IAlianzaPoliticaService alianzaPoliticaService, IPartidoPoliticoService partidoPoliticoService, IMapper mapper)
        {
            _alianzaPoliticaService = alianzaPoliticaService;
            _partidoPoliticoService = partidoPoliticoService;
            _mapper = mapper;
        }

        private int GetPartidoId()
        {
            var claim = User.FindFirst("PartidoPoliticoId");
            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public async Task<IActionResult> Index()
        {
            int partidoId = GetPartidoId();
            if (partidoId == 0)
            {
                ViewBag.SinPartido = true;
                return View(new List<AlianzaPoliticaViewModel>());
            }

            var dtos = await _alianzaPoliticaService.GetAllByPartidoIdAsync(partidoId);
            var vms = _mapper.Map<List<AlianzaPoliticaViewModel>>(dtos);
            return View(vms);
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save()
        {
            int partidoId = GetPartidoId();
            if (partidoId == 0)
            {
                return RedirectToRoute(new { controller = "AlianzaPolitica", action ="Index" });
            }

            SaveAlianzaPoliticaViewModel vm = new() { PartidoSolicitanteId = partidoId };
            await LoadPartidosDisponibles(vm, partidoId);
            return View(vm);
        }

        [HttpPost]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save(SaveAlianzaPoliticaViewModel vm)
        {
            int partidoId = GetPartidoId();
            if (partidoId == 0)
            {
                return RedirectToRoute(new { controller = "AlianzaPolitica", action ="Index" });
            }

            vm.PartidoSolicitanteId = partidoId;

            if (!ModelState.IsValid)
            {
                await LoadPartidosDisponibles(vm, partidoId);
                return View(vm);
            }

            bool existe = await _alianzaPoliticaService.GetAlianzaAsync(vm.PartidoSolicitanteId, vm.PartidoReceptorId, vm.Id);
            if (existe)
            {
                ModelState.AddModelError("PartidoAliadoId", "Ya existe una alianza registrada con este partido político.");
                await LoadPartidosDisponibles(vm, partidoId);
                return View(vm);
            }

            SaveAlianzaPoliticaDto dto = _mapper.Map<SaveAlianzaPoliticaDto>(vm);

            if (vm.Id == 0)
            {
                await _alianzaPoliticaService.AddAsync(dto);
            }
            else
            {
                await _alianzaPoliticaService.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new { controller = "AlianzaPolitica", action = "Index" });
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var dto = await _alianzaPoliticaService.GetByIdAsync(id);
            if (dto != null && dto.PartidoSolicitanteId == GetPartidoId())
            {
                await _alianzaPoliticaService.ChangeStatusAsync(id);
            }
            return RedirectToRoute(new { controller = "AlianzaPolitica", action = "Index" });
        }

        private async Task LoadPartidosDisponibles(SaveAlianzaPoliticaViewModel vm, int partidoId)
        {
            var todosPartidos = await _partidoPoliticoService.GetAllViewModelAsync();
            var partidosFiltrados = todosPartidos.Where(p => p.Id != partidoId).ToList();
            vm.PartidosDisponibles = _mapper.Map<List<PartidoPoliticoViewModel>>(partidosFiltrados);
        }
    }
}
