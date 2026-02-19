using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eVote360App.Core.Application.Viewmodels.PartidosPoliticos
{
    public class SavePartidoPoliticoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del partido es obligatorio.")]
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Las siglas del partido son obligatorias.")]
        public required string Siglas { get; set; }
        public string? Logo { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
