
using Dominio.Entidad;


namespace Infraestructura.Repositorio.Interfaz
{
    public interface IUsuarioRepo
    {
        Task<ICollection<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioByUsuario(int Id);
        Task<bool> ValidadUsuario(string Usuario);
        Task<Usuario> Login(Usuario usuarioLogin);
        Task<Usuario> Registros(Usuario UsuarioLogin);
    }
}
