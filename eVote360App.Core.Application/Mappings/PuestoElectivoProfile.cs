using AutoMapper;
using eVote360App.Core.Application.Dtos.PuestoElectivo;
using eVote360App.Core.Application.Viewmodels.PuestosElectivos;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class PuestoElectivoProfile : Profile
    {
        public PuestoElectivoProfile()
        {
            CreateMap<PuestoElectivo, PuestoElectivoDto>().ReverseMap();
            CreateMap<PuestoElectivo, SavePuestoElectivoDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Candidatos, opt => opt.Ignore());

            CreateMap<PuestoElectivoDto, PuestoElectivoViewModel>().ReverseMap();
            CreateMap<SavePuestoElectivoDto, SavePuestoElectivoViewModel>().ReverseMap();
        }
    }
}
