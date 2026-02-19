using AutoMapper;
using eVote360App.Core.Application.Dtos.Usuarios;
using eVote360App.Core.Application.Viewmodels.Usuarios;
using eVote360App.Core.Domain.Common.Enums;
using eVote360App.Core.Domain.Entities;

namespace eVote360App.Core.Application.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<LoginViewModel, LoginRequestDto>();
            CreateMap<UsuarioDto, UsuarioViewModel>()
                .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src 
            => src.Rol == (int)RolUsuario.Administrador ? "Administrador" : "DirigentePolitico"));
            CreateMap<SaveUsuarioDto, SaveUsuarioViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<SaveAsignacionViewModel, UpdateAsignacionDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => (int)src.Rol))
                .ForMember(dest => dest.PartidoNombre, opt => opt.MapFrom(src => src.PartidoPolitico != null ? src.PartidoPolitico.Nombre : "Sin Asignar"))
                .ForMember(dest => dest.PartidoLogo, opt => opt.MapFrom(src => src.PartidoPolitico != null ? src.PartidoPolitico.Logo : null));

            CreateMap<LoginRequestDto, Usuario>()
                .ForMember(dest => dest.Nombre, opt => opt.Ignore())
                .ForMember(dest => dest.Apellido, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPolitico, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPoliticoId, opt => opt.Ignore());

            CreateMap<Usuario, SaveUsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => (int)src.Rol))
                .ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPolitico, opt => opt.Ignore());
        }
    }
}
