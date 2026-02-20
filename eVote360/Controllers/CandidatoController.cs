using AutoMapper;
using eVote360App.Core.Application.Dtos.Candidatos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Candidato;
using eVote360App.Core.Application.Viewmodels.PuestosElectivos;
using eVote360App.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "DirigentePolitico")]
    public class CandidatoController : Controller
    {
        private readonly ICandidatoService _candidatoService;
        private readonly IPuestoElectivoService _puestoElectivoService;
        private readonly IMapper _mapper;
        public CandidatoController(ICandidatoService candidatoService, IPuestoElectivoService puestoElectivoService, IMapper mapper)
        {
            _candidatoService = candidatoService;
            _puestoElectivoService = puestoElectivoService;
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
                return View(new List<CandidatoViewModel>());
            }

            var dtos = await _candidatoService.GetAllByPartidoIdAsync(partidoId);
            var vms = _mapper.Map<List<CandidatoViewModel>>(dtos);
            return View(vms);
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save()
        {
            int partidoId = GetPartidoId();
            if (partidoId == 0)
            {
                return RedirectToRoute(new { controller = "Candidato",action = "Index" });
            } 

            SaveCandidatoViewModel vm = new() { Nombre = "", Apellido = ""};
            var puestosDto = await _puestoElectivoService.GetAllViewModelAsync();
            vm.Puestos = _mapper.Map<List<PuestoElectivoViewModel>>(puestosDto);
            return View(vm);
        }

        [HttpPost]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save(SaveCandidatoViewModel vm)
        {
            int partidoId = GetPartidoId();
            if (partidoId == 0)
            {
                return RedirectToRoute(new { controller = "Candidato", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                var puestosDto = await _puestoElectivoService.GetAllViewModelAsync();
                vm.Puestos = _mapper.Map<List<PuestoElectivoViewModel>>(puestosDto);
                return View(vm);
            }

            SaveCandidatoDto dto = _mapper.Map<SaveCandidatoDto>(vm);
            dto.PartidoPoliticoId = partidoId;
            dto.Foto = vm.Foto ?? string.Empty;

            if (vm.File != null && vm.File.Length > 0)
            {
                string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Candidatos");
                if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.File.FileName);
                string filePath = Path.Combine(basePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.File.CopyToAsync(stream);
                }
                dto.Foto = $"/Images/Candidatos/{fileName}";
            }
            else if (vm.Id == 0)
            {
                ModelState.AddModelError("File", "Debe adjuntar la foto del candidato.");
                var puestosDto = await _puestoElectivoService.GetAllViewModelAsync();
                vm.Puestos = _mapper.Map<List<PuestoElectivoViewModel>>(puestosDto);
                return View(vm);
            }

            if (vm.Id == 0) 
            {
                await _candidatoService.AddAsync(dto);
            }
            else
            {
                await _candidatoService.UpdateAsync(dto, vm.Id);
            } 

            return RedirectToRoute(new { controller = "Candidato", action = "Index" });
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _candidatoService.GetByIdAsync(id);
            if (dto == null || dto.PartidoPoliticoId != GetPartidoId())
            {
                RedirectToRoute(new { controller = "Candidato", action = "Index" });
            } 

            SaveCandidatoViewModel vm = _mapper.Map<SaveCandidatoViewModel>(dto);
            var puestosDto = await _puestoElectivoService.GetAllViewModelAsync();
            vm.Puestos = _mapper.Map<List<PuestoElectivoViewModel>>(puestosDto);

            return View("Save", vm);
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var dto = await _candidatoService.GetByIdAsync(id);
            if (dto != null && dto.PartidoPoliticoId == GetPartidoId())
            {
                await _candidatoService.ChangeStatusAsync(id);
            }
            return RedirectToRoute(new { controller = "Candidato", action = "Index" });
        }
    }
}
