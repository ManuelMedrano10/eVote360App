using AutoMapper;
using eVote360App.Core.Application.Dtos.PartidosPoliticos;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class PartidoPoliticoProfile : Profile
    {
        public PartidoPoliticoProfile()
        {
            CreateMap<PartidoPolitico, PartidoPoliticoDto>().ReverseMap();
            CreateMap<PartidoPolitico, SavePartidoPoliticoDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Candidatos, opt => opt.Ignore())
                .ForMember(x => x.Dirigentes, opt => opt.Ignore());

            CreateMap<PartidoPoliticoDto, PartidoPoliticoViewModel>().ReverseMap();
            CreateMap<SavePartidoPoliticoDto, SavePartidoPoliticoViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
