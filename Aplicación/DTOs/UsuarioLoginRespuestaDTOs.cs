

using Dominio.Entidad;

namespace Aplicación.DTOs
{
    public class UsuarioLoginRespuestaDTOs
    {
        public Usuario? usuarios { get; set; }
        //public UsuariosDatosDTOs? usuarios {  get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
