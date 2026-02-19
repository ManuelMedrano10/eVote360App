namespace eVote360App.Core.Application.Viewmodels.Eleccion
{
    public class EleccionViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public bool Finalizada { get; set; }
        public bool IsActive { get; set; }
    }
}
