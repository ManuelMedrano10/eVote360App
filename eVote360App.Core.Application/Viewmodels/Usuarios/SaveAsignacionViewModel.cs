using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;
using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Usuarios
{
    public class SaveAsignacionViewModel
    {
        public int UsuarioId { get; set; }
        public string? NombreDirigente { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un partido político.")]
        public int PartidoPoliticoId { get; set; }
        public List<PartidoPoliticoViewModel>? Partidos { get; set; }
    }
}
