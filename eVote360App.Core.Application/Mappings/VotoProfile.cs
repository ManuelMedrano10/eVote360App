using AutoMapper;
using eVote360App.Core.Application.Dtos.Votos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class VotoProfile : Profile
    {
        public VotoProfile()
        {
            CreateMap<SaveVotoDto, Voto>().ReverseMap();
        }
    }
}
