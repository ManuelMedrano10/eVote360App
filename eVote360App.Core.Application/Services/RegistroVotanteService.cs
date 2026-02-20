using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eVote360App.Core.Application.Dtos.RegistroVotante;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Core.Application.Services
{
    public class RegistroVotanteService : IRegistroVotanteService
    {
        private readonly IRegistroVotanteRepository _repository;
        private readonly IMapper _mapper;
        public RegistroVotanteService(IRegistroVotanteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> HaVotadoAsync(int ciudadanoId, int eleccionId)
        {
            return await _repository.GetAllQuery()
                .AnyAsync(r => r.CiudadanoId == ciudadanoId && r.EleccionId == eleccionId);
        }

        public async Task RegistrarVotoAsync(SaveRegistroVotanteDto dto)
        {
            var registro = _mapper.Map<RegistroVotante>(dto);
            await _repository.AddAsync(registro);
        }
    }
}
