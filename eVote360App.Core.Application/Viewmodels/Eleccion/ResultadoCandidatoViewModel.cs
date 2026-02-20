namespace eVote360App.Core.Application.Viewmodels.Eleccion
{
    public class ResultadoCandidatoViewModel
    {
        public required string NombreCompleto { get; set; }
        public required string Partido { get; set; }
        public required string LogoPartido { get; set; }
        public int VotosObtenidos { get; set; }
        public double Porcentaje { get; set; }
    }
}
