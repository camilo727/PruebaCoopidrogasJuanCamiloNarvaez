using Aplicación.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicación.Servicios.Interfaz
{
    public interface IClienteServe
    {
        Task<ICollection<CleinteDTOs>> GetCliente();
        Task<CleinteDTOs> GetClienteById(int Id);
        Task<bool> ExisteCliente(int Id);
        Task<bool> ExisteNombreCliente(string Nombre);
        Task<bool> CrearCliente(CrearCleinteDTOs cliente);
        Task<bool> ActualCliente(CrearCleinteDTOs crearClienteDTOs, int Id);
        Task<bool> EliminarCliente(int Id);
    }
}
