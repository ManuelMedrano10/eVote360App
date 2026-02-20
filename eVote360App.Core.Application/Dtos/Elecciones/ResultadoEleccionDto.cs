namespace eVote360App.Core.Application.Dtos.Elecciones
{
    public class ResultadoEleccionDto
    {
        public required string NombreEleccion { get; set; }
        public List<ResultadoPuestoDto> Puestos { get; set; } = new();
    }
}
