using eVote360App.Core.Application.Dtos.Elecciones;

namespace eVote360App.Core.Application.Viewmodels.Eleccion
{
    public class ResultadoEleccionViewModel
    {
        public required string NombreEleccion { get; set; }
        public List<ResultadoPuestoViewModel> Puestos { get; set; } = new();
    }
}
