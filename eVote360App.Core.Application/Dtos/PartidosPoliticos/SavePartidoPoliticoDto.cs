namespace eVote360App.Core.Application.Dtos.PartidosPoliticos
{
    public class SavePartidoPoliticoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required string Siglas { get; set; }
        public required string Logo { get; set; }
    }
}
