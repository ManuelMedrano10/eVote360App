namespace eVote360App.Core.Application.Dtos.PuestoElectivo
{
    public class SavePuestoElectivoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
    }
}
