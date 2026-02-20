using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Ciudadano
{
    public class VerifyCiudadanoViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su documento de identidad.")]
        public required string DocumentoIdentidad { get; set; }
    }
}
