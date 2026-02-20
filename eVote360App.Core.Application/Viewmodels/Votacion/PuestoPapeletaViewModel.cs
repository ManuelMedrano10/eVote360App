using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Votacion
{
    public class PuestoPapeletaViewModel
    {
        public int PuestoId { get; set; }
        public string PuestoNombre { get; set; } = null!;
        public List<CandidatoPapeletaViewModel> Candidatos { get; set; } = new();
        [Required(ErrorMessage = "Debe seleccionar un candidato o la opción de 'Ninguno'")]
        public int SeleccionCandidatoId { get; set; }
    }
}
