using AutoMapper;
using eVote360App.Core.Application.Dtos.Alianzas;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class AlianzaPoliticaService : GenericService<SaveAlianzaPoliticaDto, AlianzaPoliticaDto, AlianzaPolitica>, IAlianzaPoliticaService
    {
        private readonly IAlianzaPoliticaRepository _repository;
        private readonly IMapper _mapper;
        public AlianzaPoliticaService(IAlianzaPoliticaRepository repository, IMapper mapper)
            :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> GetAlianzaAsync(int partido1, int partido2, int currentId = 0)
        {
            var query = _repository.GetAllQuery()
                .Where(a => (a.PartidoSolicitanteId == partido1 && a.PartidoReceptorId == partido2) ||
                            (a.PartidoSolicitanteId == partido2 && a.PartidoReceptorId == partido1));

            if (currentId != 0) query = query.Where(a => a.Id != currentId);

            return await query.AnyAsync();
        }

        public async Task<List<AlianzaPoliticaDto>> GetAllByPartidoIdAsync(int partidoId)
        {
            var list = await _repository.GetAllQuery()
                .Include(a => a.PartidoReceptor)
                .Where(a => a.PartidoSolicitanteId == partidoId)
                .ToListAsync();

            return _mapper.Map<List<AlianzaPoliticaDto>>(list);
        }
    }
}
