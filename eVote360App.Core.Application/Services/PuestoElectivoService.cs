using AutoMapper;
using eVote360App.Core.Application.Dtos.PuestoElectivo;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;

namespace eVote360App.Core.Application.Services
{
    public class PuestoElectivoService : GenericService<SavePuestoElectivoDto, PuestoElectivoDto, PuestoElectivo>, IPuestoElectivoService
    {
        private readonly IPuestoElectivoRepository _repository;
        private readonly IMapper _mapper;
        public PuestoElectivoService(IPuestoElectivoRepository repository, IMapper mapper)
            :base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
