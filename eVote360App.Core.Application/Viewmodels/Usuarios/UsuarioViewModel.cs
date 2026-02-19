using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360App.Core.Application.Viewmodels.Usuarios
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string NombreUsuario { get; set; }
        public required string RolNombre { get; set; }
        public bool IsActive { get; set; }
    }
}
