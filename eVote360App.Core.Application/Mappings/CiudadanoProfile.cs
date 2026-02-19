using AutoMapper;
using eVote360App.Core.Application.Dtos.Ciudadanos;
using eVote360App.Core.Application.Viewmodels.Ciudadano;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class CiudadanoProfile : Profile
    {
        public CiudadanoProfile()
        {
            CreateMap<Ciudadano, CiudadanoDto>().ReverseMap();
            CreateMap<Ciudadano, SaveCiudadanoDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore());

            CreateMap<CiudadanoDto, CiudadanoViewModel>().ReverseMap();
            CreateMap<SaveCiudadanoDto, SaveCiudadanoViewModel>().ReverseMap();
        }
    }
}
