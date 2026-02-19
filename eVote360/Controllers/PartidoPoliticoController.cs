using AutoMapper;
using eVote360App.Core.Application.Dtos.PartidosPoliticos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoService _service;
        private readonly IMapper _mapper;
        public PartidoPoliticoController(IPartidoPoliticoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            var partidosDto = await _service.GetAllViewModelAsync();

            var partidosVm = _mapper.Map<List<PartidoPoliticoViewModel>>(partidosDto);

            return View(partidosVm);
        }

        public IActionResult Save()
        {
            return View(new SavePartidoPoliticoViewModel() {Id = 0, Nombre = "", Siglas = "", Logo = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Save(SavePartidoPoliticoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            bool existeSigla = await _service.GetSiglaAsync(vm.Siglas, vm.Id);
            if (existeSigla)
            {
                ModelState.AddModelError("Siglas", "Ya existe un partido registrado con estas siglas.");
                return View(vm);
            }

            SavePartidoPoliticoDto dto = _mapper.Map<SavePartidoPoliticoDto>(vm);
            dto.Logo = vm.Logo ?? string.Empty;

            if (vm.File != null && vm.File.Length > 0)
            {
                string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Partidos");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.File.FileName);
                string filePath = Path.Combine(basePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.File.CopyToAsync(stream);
                }

                dto.Logo = $"/Images/Partidos/{fileName}";
            }
            else if (vm.Id == 0)
            {
                ModelState.AddModelError("File", "Debe adjuntar el logo del partido político.");
                return View(vm);
            }

            if (vm.Id == 0)
            {
                await _service.AddAsync(dto);
            }
            else
            {
                await _service.UpdateAsync(dto, vm.Id);
            }

            return RedirectToRoute(new {controller = "PartidoPolitico", action = "Index"});
        }

        public async Task<IActionResult> Edit(int id)
        {
            var partido = await _service.GetByIdAsync(id);
            if (partido == null)
            {
                return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
            }

            SavePartidoPoliticoViewModel vm = _mapper.Map<SavePartidoPoliticoViewModel>(partido);

            return View("Save", vm);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _service.ChangeStatusAsync(id);
            return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
        }
    }
}
