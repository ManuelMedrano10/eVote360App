namespace eVote360App.Core.Application.Viewmodels.Eleccion
{
    public class ResultadoPuestoViewModel
    {
        public required string Puesto { get; set; }
        public List<ResultadoCandidatoViewModel> Candidatos { get; set; } = new();
    }
}
