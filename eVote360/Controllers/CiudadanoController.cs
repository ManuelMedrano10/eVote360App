using AutoMapper;
using eVote360App.Core.Application.Dtos.Ciudadanos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.Ciudadano;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CiudadanoController : Controller
    {
        private readonly ICiudadanoService _service;
        private readonly IMapper _mapper;

        public CiudadanoController(ICiudadanoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllViewModelAsync();
            var vms = _mapper.Map<List<CiudadanoViewModel>>(dtos);

            return View(vms);
        }

        public IActionResult Save()
        {
            return View(new SaveCiudadanoViewModel() 
            { 
                Id = 0, 
                Nombre = "", 
                Apellido = "", 
                DocumentoIdentidad = "", 
                Email = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaveCiudadanoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            bool existeDocumento = await _service.GetDocumentoAsync(vm.DocumentoIdentidad, vm.Id);
            if (existeDocumento)
            {
                ModelState.AddModelError("DocumentoIdentidad", "Ya existe un ciudadano registrado con este numero de identidad.");
                return View(vm);
            }

            SaveCiudadanoDto dto = _mapper.Map<SaveCiudadanoDto>(vm);

            if (vm.Id == 0)
            {
                await _service.AddAsync(dto);
            }
            else
            {
                await _service.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) 
            { 
                return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
            }

            SaveCiudadanoViewModel vm = _mapper.Map<SaveCiudadanoViewModel>(dto);
            return View("Save", vm);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _service.ChangeStatusAsync(id);
            return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
        }
    }
}
