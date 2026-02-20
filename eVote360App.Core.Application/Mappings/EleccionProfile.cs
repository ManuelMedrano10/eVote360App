using AutoMapper;
using eVote360App.Core.Application.Dtos.Elecciones;
using eVote360App.Core.Application.Viewmodels.Eleccion;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class EleccionProfile : Profile
    {
        public EleccionProfile()
        {
            CreateMap<Eleccion, EleccionDto>().ReverseMap();

            CreateMap<Eleccion, SaveEleccionDto>()
                    .ReverseMap()
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.Finalizada, opt => opt.Ignore());

            CreateMap<EleccionDto, EleccionViewModel>().ReverseMap();
            CreateMap<SaveEleccionDto, SaveEleccionViewModel>().ReverseMap();
            CreateMap<ResultadoEleccionDto, ResultadoEleccionViewModel>().ReverseMap();
            CreateMap<ResultadoPuestoDto, ResultadoPuestoViewModel>().ReverseMap();
            CreateMap<ResultadoCandidatoDto, ResultadoCandidatoViewModel>().ReverseMap();
        }
    }
}
