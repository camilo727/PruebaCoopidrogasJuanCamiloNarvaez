
using System.ComponentModel.DataAnnotations;


namespace Aplicación.DTOs
{
    public class UsuarioLoginDTOs
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string? Password { get; set; }
    }
}
