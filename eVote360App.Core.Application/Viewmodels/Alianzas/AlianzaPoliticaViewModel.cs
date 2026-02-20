namespace eVote360App.Core.Application.Viewmodels.Alianzas
{
    public class AlianzaPoliticaViewModel
    {
        public int Id { get; set; }
        public int PartidoReceptorId { get; set; }
        public string? PartidoReceptorNombre { get; set; }
        public string? PartidoReceptorLogo { get; set; }
        public string? EstadoAlianza { get; set; }
        public bool IsActive { get; set; }
    }
}
