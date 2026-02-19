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
        private readonly IMapper _mapper;
        public EleccionService(IEleccionRepository repository, IMapper mapper)
            :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
