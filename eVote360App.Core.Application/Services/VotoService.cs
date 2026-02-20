using AutoMapper;
using eVote360App.Core.Application.Dtos.Votos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;

namespace eVote360App.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _votoRepository;
        private readonly IMapper _mapper;

        public VotoService(IVotoRepository votoRepository, IMapper mapper)
        {
            _votoRepository = votoRepository;
            _mapper = mapper;
        }
        public async Task EmitirVotosAsync(List<SaveVotoDto> votos)
        {
            foreach (var votoDto in votos)
            {
                var voto = _mapper.Map<Voto>(votoDto);
                await _votoRepository.AddAsync(voto);
            }
        }
    }
}
