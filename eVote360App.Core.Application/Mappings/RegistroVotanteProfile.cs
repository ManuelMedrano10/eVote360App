using AutoMapper;
using eVote360App.Core.Application.Dtos.RegistroVotante;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class RegistroVotanteProfile : Profile
    {
        public RegistroVotanteProfile()
        {
            CreateMap<SaveRegistroVotanteDto, RegistroVotante>().ReverseMap();
        }
    }
}
