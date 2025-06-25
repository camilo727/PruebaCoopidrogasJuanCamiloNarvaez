using Aplicación.DTOs;
using Dominio.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicación.Servicios.Interfaz
{
    public interface IUsuarioServer
    {
        Task<ICollection<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioByUsuario(int Id);
        Task<bool> ValidadUsuario(string Usuario);
        Task<UsuarioLoginRespuestaDTOs> Login(UsuarioLoginDTOs usuarioLoginDTOs);
        Task<Usuario> Registros(UsuarioRegistroDTOs usuarioLoginDTOs);
    }
}
