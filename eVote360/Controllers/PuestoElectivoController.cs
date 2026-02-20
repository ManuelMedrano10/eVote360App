using AutoMapper;
using eVote360App.Core.Application.Dtos.PuestoElectivo;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.PuestosElectivos;
using eVote360App.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PuestoElectivoController : Controller
    {
        private readonly IPuestoElectivoService _service;
        private readonly IMapper _mapper;
        public PuestoElectivoController(IPuestoElectivoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var puestosDto = await _service.GetAllViewModelAsync();
            var puestosVm = _mapper.Map<List<PuestoElectivoViewModel>>(puestosDto);

            return View(puestosVm);
        }
        [BlockDuringElectionAtribute]
        public IActionResult Save()
        {
            return View(new SavePuestoElectivoViewModel() { Id = 0, Nombre = "", Descripcion = "" });
        }

        [HttpPost]
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Save(SavePuestoElectivoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            SavePuestoElectivoDto dto = _mapper.Map<SavePuestoElectivoDto>(vm);

            if (vm.Id == 0)
            {
                await _service.AddAsync(dto);
            }
            else
            {
                await _service.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
        }
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Edit(int id)
        {
            var puestoDto = await _service.GetByIdAsync(id);
            if (puestoDto == null)
            {
                return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
            }

            SavePuestoElectivoViewModel vm = _mapper.Map<SavePuestoElectivoViewModel>(puestoDto);
            return View("Save", vm);
        }
        [BlockDuringElectionAtribute]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _service.ChangeStatusAsync(id);
            return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
        }
    }
}
