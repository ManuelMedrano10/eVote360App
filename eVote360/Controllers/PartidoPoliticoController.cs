using eVote360App.Core.Application.Dtos.PartidosPoliticos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVote360App.Controllers
{
    [Authorize(Roles = "Adiministrador")]
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoService _service;
        public PartidoPoliticoController(IPartidoPoliticoService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var partidos = await _service.GetAllViewModelAsync();
            return View(partidos);
        }

        public IActionResult Save()
        {
            return View(new SavePartidoPoliticoViewModel() { Nombre = "", Siglas = "", Logo = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePartidoPoliticoViewModel vm)
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

            SavePartidoPoliticoDto dto = new ()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Siglas = vm.Siglas,
                Logo = vm.Logo ?? string.Empty
            };

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

            SavePartidoPoliticoViewModel vm = new()
            {
                Id = partido.Id,
                Nombre = partido.Nombre,
                Descripcion = partido.Descripcion,
                Siglas = partido.Siglas,
                Logo = partido.Logo
            };

            return View("Create", vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
        }
    }
}
