
using System.ComponentModel.DataAnnotations;


namespace Aplicación.DTOs
{
    public class UsuarioRegistroDTOs
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
