using AutoMapper;
using eVote360App.Core.Application.Dtos.Alianzas;
using eVote360App.Core.Application.Viewmodels.Alianzas;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class AlianzaPoliticaProfile : Profile
    {
        public AlianzaPoliticaProfile()
        {
            CreateMap<AlianzaPolitica, AlianzaPoliticaDto>()
                .ForMember(dest => dest.PartidoReceptorNombre, opt => opt.MapFrom(src => src.PartidoReceptor != null ? src.PartidoReceptor.Nombre : string.Empty))
                .ForMember(dest => dest.PartidoReceptorLogo, opt => opt.MapFrom(src => src.PartidoReceptor != null ? src.PartidoReceptor.Logo : string.Empty))
                .ForMember(dest => dest.EstadoAlianza, opt => opt.MapFrom(src => src.Estado.ToString()))
                .ReverseMap()
                .ForMember(x => x.PartidoSolicitante, opt => opt.Ignore())
                .ForMember(x => x.PartidoReceptor, opt => opt.Ignore());

            CreateMap<AlianzaPolitica, SaveAlianzaPoliticaDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Estado, opt => opt.Ignore()) 
                .ForMember(x => x.FechaSolicitud, opt => opt.Ignore())
                .ForMember(x => x.FechaAceptacion, opt => opt.Ignore())
                .ForMember(x => x.PartidoSolicitante, opt => opt.Ignore())
                .ForMember(x => x.PartidoReceptor, opt => opt.Ignore());

            CreateMap<AlianzaPoliticaDto, AlianzaPoliticaViewModel>().ReverseMap();
            CreateMap<SaveAlianzaPoliticaDto, SaveAlianzaPoliticaViewModel>()
                .ForMember(x => x.PartidosDisponibles, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
