namespace eVote360App.Core.Application.Dtos.Alianzas
{
    public class SaveAlianzaPoliticaDto
    {
        public int Id { get; set; }
        public int PartidoSolicitanteId { get; set; }
        public int PartidoReceptorId { get; set; }
    }
}
