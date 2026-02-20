using AutoMapper;
using eVote360App.Core.Application.Dtos.Elecciones;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Eleccion;
using eVote360App.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EleccionController : Controller
    {
        private readonly IEleccionService _service;
        private readonly IMapper _mapper;
        public EleccionController(IEleccionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllViewModelAsync();
            var vms = _mapper.Map<List<EleccionViewModel>>(dtos)
                             .OrderByDescending(e => e.FechaRealizacion)
                             .ToList();

            return View(vms);
        }

        public IActionResult Save()
        {
            return View(new SaveEleccionViewModel() { Id = 0, Nombre = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaveEleccionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            bool existeActiva = await _service.GetEleccionActivaAsync(vm.Id);
            if (existeActiva)
            {
                ModelState.AddModelError("", "No se puede guardar esta elección porque ya existe otra elección en curso.");
                return View(vm);
            }

            SaveEleccionDto dto = _mapper.Map<SaveEleccionDto>(vm);

            if (vm.Id == 0)
            {
                await _service.AddAsync(dto);
            }
            else
            {
                await _service.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new { controller = "Eleccion", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return RedirectToAction("Index");

            SaveEleccionViewModel vm = _mapper.Map<SaveEleccionViewModel>(dto);
            return View("Save", vm);
        }

        public async Task<IActionResult> Finalizar(int id)
        {
            await _service.FinalizarEleccionAsync(id);
            return RedirectToRoute(new { controller = "Eleccion", action = "Index" });
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _service.ChangeStatusAsync(id);
            return RedirectToRoute(new { controller = "Eleccion", action = "Index" });
        }

        [BlockDuringElectionAtribute]
        public async Task<IActionResult> Resultado(int id)
        {
            var dto = await _service.GetResultadosAsync(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Eleccion", action = "Index" });
            } 
                
            var vm = _mapper.Map<ResultadoEleccionViewModel>(dto);
            return View(vm);
        }
    }
}
