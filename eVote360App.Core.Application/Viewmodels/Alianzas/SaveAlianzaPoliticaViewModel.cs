using System.ComponentModel.DataAnnotations;
using eVote360App.Core.Application.Viewmodels.PartidosPoliticos;

namespace eVote360App.Core.Application.Viewmodels.Alianzas
{
    public class SaveAlianzaPoliticaViewModel
    {
        public int Id { get; set; }
        public int PartidoSolicitanteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un partido político.")]
        public int PartidoReceptorId { get; set; }
        public List<PartidoPoliticoViewModel>? PartidosDisponibles { get; set; }
    }
}
