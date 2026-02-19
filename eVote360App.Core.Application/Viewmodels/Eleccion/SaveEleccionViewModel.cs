using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.Eleccion
{
    public class SaveEleccionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la elección es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "Debe especificar la fecha de realización.")]
        [DataType(DataType.Date)]
        public DateTime FechaRealizacion { get; set; } = DateTime.Today;
    }
}
