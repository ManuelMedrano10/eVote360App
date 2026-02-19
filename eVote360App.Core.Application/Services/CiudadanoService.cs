using AutoMapper;
using eVote360App.Core.Application.Dtos.Ciudadanos;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class CiudadanoService : GenericService<SaveCiudadanoDto, CiudadanoDto, Ciudadano>, ICiudadanoService
    {
        private readonly ICiudadanoRepository _repository;
        private readonly IMapper _mapper;
        public CiudadanoService(ICiudadanoRepository repository, IMapper mapper)
            :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> GetDocumentoAsync(string documento, int currentId = 0)
        {
            var query = _repository.GetAllQuery().Where(c => c.DocumentoIdentidad == documento);

            if(currentId != 0)
            {
                query = query.Where(c => c.Id != currentId);
            }

            return await query.AnyAsync();
        }
    }
}
