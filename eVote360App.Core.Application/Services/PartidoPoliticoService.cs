using AutoMapper;
using eVote360App.Core.Application.Dtos.PartidosPoliticos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class PartidoPoliticoService : GenericService<SavePartidoPoliticoDto, PartidoPoliticoDto, PartidoPolitico>, IPartidoPoliticoService
    {
        private readonly IPartidoPoliticoRepository _repository;
        private readonly IMapper _mapper;
        public PartidoPoliticoService(IPartidoPoliticoRepository repository, IMapper mapper)
            :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> GetSiglaAsync(string siglas, int currentId = 0)
        {
            var query = _repository.GetAllQuery().Where(p => p.Siglas == siglas && p.IsActive == true);

            if (currentId != 0)
            {
                query = query.Where(p => p.Id != currentId);
            }

            return await query.AnyAsync();
        }
    }
}
