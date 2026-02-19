namespace eVote360App.Core.Application.Dtos.Elecciones
{
    public class EleccionDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public bool Finalizada { get; set; }
        public bool IsActive { get; set; }
    }
}
