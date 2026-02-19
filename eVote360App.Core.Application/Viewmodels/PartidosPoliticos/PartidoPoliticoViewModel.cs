namespace eVote360App.Core.Application.Viewmodels.PartidosPoliticos
{
    public class PartidoPoliticoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Siglas { get; set; }
        public required string Logo { get; set; }
        public bool IsActive { get; set; }
    }
}
