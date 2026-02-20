using AutoMapper;
using eVote360App.Core.Application.Dtos.Candidatos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class CandidatoService : GenericService<SaveCandidatoDto, CandidatoDto, Candidato>, ICandidatoService
    {
        private readonly ICandidatoRepository _repository;
        private readonly IMapper _mapper;
        public CandidatoService(ICandidatoRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CandidatoDto>> GetAllByPartidoIdAsync(int partidoId)
        {
            var list = await _repository.GetAllQuery()
                .Include(c => c.PuestoElectivo)
                .Where(c => c.PartidoPoliticoId == partidoId)
                .ToListAsync();

            return _mapper.Map<List<CandidatoDto>>(list);
        }
        public async Task<List<CandidatoDto>> GetAllActivosParaVotacionAsync()
        {
            var list = await _repository.GetAllQuery()
                .Include(c => c.PuestoElectivo)
                .Include(c => c.PartidoPolitico)
                .Where(c => c.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<CandidatoDto>>(list);
        }
    }
}
