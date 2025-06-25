using Dominio.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorio.Interfaz
{
    public interface IClienteRepo
    {
        Task<ICollection<Cliente>> GetClientes();
        Task<Cliente>GetCleinteById(int id);
        Task<bool> ValidaCliente(int Id);
        Task<bool> ExisteCliente(int Id);
        Task<bool> ExisteNombreCliente(string Nombre);
        Task<bool> CrearCliente(Cliente cliente);
        Task<bool> ActualCliente(Cliente cliente);
        Task<bool> EliminarCliente(Cliente cliente);
        Task<bool> GuadarCliente();
    }
}
