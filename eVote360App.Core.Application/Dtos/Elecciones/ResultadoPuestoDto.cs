namespace eVote360App.Core.Application.Dtos.Elecciones
{
    public class ResultadoPuestoDto
    {
        public required string Puesto { get; set; }
        public List<ResultadoCandidatoDto> Candidatos { get; set; } = new();
    }
}
