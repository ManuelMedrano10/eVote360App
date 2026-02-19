using System.ComponentModel.DataAnnotations;

namespace eVote360App.Core.Application.Viewmodels.PuestosElectivos
{
    public class SavePuestoElectivoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del puesto electivo es obligatorio.")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción del puesto electivo es obligatoria.")]
        public required string Descripcion { get; set; }
    }
}
