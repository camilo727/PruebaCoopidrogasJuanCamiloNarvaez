using Dominio.Entidad;
using Infraestructura.Datos;
using Infraestructura.Repositorio.Interfaz;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorio.Repositorio
{
    public class ClienteRepo : IClienteRepo
    {
        private readonly AppDbContext _conexionDb;
        public ClienteRepo(AppDbContext appDbContext)
        {
            _conexionDb = appDbContext;
        }
        public async Task<bool> ActualCliente(Cliente cliente)
        {

            Cliente CleinteExiste = _conexionDb.cliente.Find(cliente.Id);
            if (CleinteExiste != null) 
            {
                _conexionDb.Entry(CleinteExiste).CurrentValues.SetValues(cliente);
            }
            else
            {
                _conexionDb.cliente.Update(cliente);   
            }
            return await GuadarCliente();
        }

        public async Task<bool> CrearCliente(Cliente cliente)
        {
            try
            {
                _conexionDb.cliente.AddAsync(cliente);
                return await GuadarCliente();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error en la funcion de crear cliente {ex.Message}");
                return false;

            }
        }

        public async Task<bool> EliminarCliente(Cliente cliente)
        {
            try
            {
                _conexionDb.Remove(cliente);
                return await GuadarCliente(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un errror en la funcion en eliminar cliente {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExisteCliente(int Id)
        {
            try
            {
                bool Valor = await _conexionDb.cliente.AnyAsync(x => x.Id == Id);
                return Valor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error en la funcion de existe cliente {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExisteNombreCliente(string Nombre)
        {
            try
            {
                bool Valor = await _conexionDb.cliente.AllAsync(a => a.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());
                return Valor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un Error en la funcion existe el nombre del cliente {ex.Message}");
                return false;
            }
        }

        public async Task<Cliente> GetCleinteById(int id)
        {

            try
            {
                return await _conexionDb.cliente.FirstOrDefaultAsync(c => c.Id == id);// Retorna la un cliente de la tabla por medio el identificador 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error en la consulata en cliente por Id {ex.Message}");
                return null;
            }
        }

        public async Task<ICollection<Cliente>> GetClientes()
        {
            try
            {
                return  await _conexionDb.cliente.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error en la consulata de en listar cliente {ex.Message}");
                return null;

            }
        }

        public Task<bool> ValidaCliente(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> GuadarCliente()
        {
            return await _conexionDb.SaveChangesAsync() >= 0 ? true : false;

        }
    }
}
