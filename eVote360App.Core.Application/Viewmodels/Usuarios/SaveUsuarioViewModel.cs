using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Usuarios
{
    public class SaveUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe poner el nombre del usuario.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "Debe poner el apellido del usuario.")]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "Debe poner el correo electronico del usuario.")]
        [EmailAddress(ErrorMessage = "El correo electronico debe ser valido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Debe poner el nombre de usuario.")]
        public required string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public int Rol { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
