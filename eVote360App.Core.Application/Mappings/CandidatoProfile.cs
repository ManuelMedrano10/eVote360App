using AutoMapper;
using eVote360App.Core.Application.Dtos.Candidatos;
using eVote360App.Core.Application.Viewmodels.Candidato;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class CandidatoProfile : Profile
    {
        public CandidatoProfile()
        {
            CreateMap<Candidato, CandidatoDto>()
                .ForMember(dest => dest.PuestoNombre, opt => opt.MapFrom(src => src.PuestoElectivo != null ? src.PuestoElectivo.Nombre : string.Empty))
                .ForMember(dest => dest.PartidoNombre, opt => opt.MapFrom(src => src.PartidoPolitico != null ? src.PartidoPolitico.Nombre : string.Empty))
                .ForMember(dest => dest.PartidoLogo, opt => opt.MapFrom(src => src.PartidoPolitico != null ? src.PartidoPolitico.Logo : string.Empty))
                .ReverseMap()
                .ForMember(x => x.PartidoPolitico, opt => opt.Ignore())
                .ForMember(x => x.PuestoElectivo, opt => opt.Ignore());

            CreateMap<Candidato, SaveCandidatoDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.PartidoPolitico, opt => opt.Ignore())
                .ForMember(x => x.PuestoElectivo, opt => opt.Ignore());

            CreateMap<CandidatoDto, CandidatoViewModel>().ReverseMap();

            CreateMap<SaveCandidatoDto, SaveCandidatoViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Puestos, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
