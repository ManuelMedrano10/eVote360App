using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Usuarios
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public required string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
