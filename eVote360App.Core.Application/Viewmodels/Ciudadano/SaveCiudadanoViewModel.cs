using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Ciudadano
{
    public class SaveCiudadanoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del ciudadano es obligatorio.")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido del ciudadano es obligatorio.")]
        public required string Apellido { get; set; }
        [Required(ErrorMessage = "El correo electronico del ciudadano es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un formato de correo valido.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El documento de identidad del ciudadano es obligatorio.")]
        public required string DocumentoIdentidad { get; set; }
    }
}
