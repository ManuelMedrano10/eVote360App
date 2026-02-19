namespace eVote360App.Core.Application.Viewmodels.PuestosElectivos
{
    public class PuestoElectivoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public bool IsActive { get; set; }
    }
}
