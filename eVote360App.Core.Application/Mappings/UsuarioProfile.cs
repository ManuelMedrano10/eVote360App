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
            CreateMap<UsuarioDto, UsuarioViewModel>()
                .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src 
            => src.Rol == (int)RolUsuario.Administrador ? "Administrador" : "DirigentePolitico"));
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => (int)src.Rol));

            CreateMap<LoginRequestDto, Usuario>()
                .ForMember(dest => dest.Nombre, opt => opt.Ignore())
                .ForMember(dest => dest.Apellido, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPolitico, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPoliticoId, opt => opt.Ignore());

            CreateMap<Usuario, SaveUsuarioViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => (int)src.Rol))
                .ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPolitico, opt => opt.Ignore())
                .ForMember(dest => dest.PartidoPoliticoId, opt => opt.Ignore());

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src => src.Rol.ToString()));
        }
    }
}
