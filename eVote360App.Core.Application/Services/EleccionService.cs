using AutoMapper;
using eVote360App.Core.Application.Dtos.Elecciones;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class EleccionService : GenericService<SaveEleccionDto, EleccionDto, Eleccion>, IEleccionService
    {
        private readonly IEleccionRepository _repository;
        private readonly IVotoRepository _votoRepository;
        private readonly IPuestoElectivoRepository _puestoRepository;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IMapper _mapper;
        public EleccionService(IEleccionRepository repository, IVotoRepository votoRepository
            , IPuestoElectivoRepository puestoRepository, ICandidatoRepository candidatoRepository
            , IMapper mapper) :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _votoRepository = votoRepository;
            _candidatoRepository = candidatoRepository;
            _puestoRepository = puestoRepository;
        }
        public async Task FinalizarEleccionAsync(int id)
        {
            var eleccion = await _repository.GetByIdAsync(id);
            if (eleccion != null)
            {
                eleccion.Finalizada = true;
                await _repository.UpdateAsync(eleccion.Id, eleccion);
            }
        }

        public async Task<bool> GetEleccionActivaAsync(int? currentEleccionId = null)
        {
            var query = _repository.GetAllQuery()
                .Where(e => e.IsActive == true && e.Finalizada == false);

            if (currentEleccionId.HasValue && currentEleccionId.Value != 0)
            {
                query = query.Where(e => e.Id != currentEleccionId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<ResultadoEleccionDto?> GetResultadosAsync(int eleccionId)
        {
            var eleccion = await _repository.GetByIdAsync(eleccionId);
            if (eleccion == null) return null;

            var resultado = new ResultadoEleccionDto
            {
                NombreEleccion = eleccion.Nombre,
                Puestos = new List<ResultadoPuestoDto>()
            };

            var votos = await _votoRepository.GetAllQuery().Where(v => v.EleccionId == eleccionId).ToListAsync();
            var puestos = await _puestoRepository.GetAllAsync();
            var candidatos = await _candidatoRepository.GetAllQuery().Include(c => c.PartidoPolitico).ToListAsync();

            foreach (var puesto in puestos)
            {
                var dtoPuesto = new ResultadoPuestoDto
                {
                    Puesto = puesto.Nombre,
                    Candidatos = new List<ResultadoCandidatoDto>()
                };

                var votosDelPuesto = votos.Where(v => v.PuestoElectivoId == puesto.Id).ToList();
                int totalVotosPuesto = votosDelPuesto.Count;

                var candidatosDelPuesto = candidatos.Where(c => c.PuestoElectivoId == puesto.Id).ToList();

                foreach (var cand in candidatosDelPuesto)
                {
                    int votosObtenidos = votosDelPuesto.Count(v => v.CandidatoId == cand.Id);
                    double porcentaje = totalVotosPuesto > 0 ? Math.Round(((double)votosObtenidos / totalVotosPuesto) * 100, 2) : 0;

                    dtoPuesto.Candidatos.Add(new ResultadoCandidatoDto
                    {
                        NombreCompleto = $"{cand.Nombre} {cand.Apellido}",
                        Partido = cand.PartidoPolitico?.Nombre ?? "Independiente",
                        LogoPartido = cand.PartidoPolitico?.Logo ?? "",
                        VotosObtenidos = votosObtenidos,
                        Porcentaje = porcentaje
                    });
                }

                dtoPuesto.Candidatos = dtoPuesto.Candidatos.OrderByDescending(c => c.VotosObtenidos).ToList();
                resultado.Puestos.Add(dtoPuesto);
            }

            return resultado;
        }
    }
}
