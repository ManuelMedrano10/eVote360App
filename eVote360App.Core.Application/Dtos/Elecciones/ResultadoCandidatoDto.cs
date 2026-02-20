namespace eVote360App.Core.Application.Dtos.Elecciones
{
    public class ResultadoCandidatoDto
    {
        public required string NombreCompleto { get; set; }
        public required string Partido { get; set; }
        public required string LogoPartido { get; set; }
        public int VotosObtenidos { get; set; }
        public double Porcentaje { get; set; }
    }
}
