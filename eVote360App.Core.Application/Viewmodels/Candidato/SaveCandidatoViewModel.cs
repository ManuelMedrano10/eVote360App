using System.ComponentModel.DataAnnotations;
using eVote360App.Core.Application.Viewmodels.PuestosElectivos;
using Microsoft.AspNetCore.Http;

namespace eVote360App.Core.Application.Viewmodels.Candidato
{
    public class SaveCandidatoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del candidato es obligatorio.")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido del candidato es obligatorio.")]
        public required string Apellido { get; set; }
        public string? Foto { get; set; }
        public int PartidoPoliticoId { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un puesto.")]
        public int PuestoElectivoId { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public List<PuestoElectivoViewModel>? Puestos { get; set; }
    }
}
