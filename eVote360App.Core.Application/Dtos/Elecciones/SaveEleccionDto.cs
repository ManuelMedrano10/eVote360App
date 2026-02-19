namespace eVote360App.Core.Application.Dtos.Elecciones
{
    public class SaveEleccionDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime FechaRealizacion { get; set; }
    }
}
