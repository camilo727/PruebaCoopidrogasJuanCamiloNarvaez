using Aplicación.DTOs;
using Aplicación.Servicios.Interfaz;
using AutoMapper;
using Dominio.Entidad;
using Infraestructura.Repositorio.Interfaz;

namespace Aplicación.Servicios.Repositorio
{
    
    public class ClienteService : IClienteServe
    {
        private readonly IClienteRepo _clienteRpo;
        private readonly IMapper _mapper;

       
        public ClienteService(IClienteRepo clienteRpo, IMapper mapper)
        {
            _clienteRpo = clienteRpo;
            _mapper = mapper;
        }
        public async Task<bool> ActualCliente(CrearCleinteDTOs crearClienteDTOs, int Id)
        {
            try
            {

                Cliente cliente = _mapper.Map<Cliente>(crearClienteDTOs);
                cliente.Id = Id;
                return await _clienteRpo.ActualCliente(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un Error en la funcion:ActualCliente {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CrearCliente(CrearCleinteDTOs clienteDtos)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(clienteDtos);
                return await _clienteRpo.CrearCliente(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" hay un error en la funcion: CrearCliente{ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarCliente(int Id)
        {

            try
            {
                Cliente cliente = await _clienteRpo.GetCleinteById(Id);
                return await _clienteRpo.EliminarCliente(cliente);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error en la funcion: EliminarCliente {ex.Message}");
                return false;

            }
        }

        public async Task<bool> ExisteCliente(int Id)
        {
            try
            {
                bool result = await _clienteRpo.ExisteCliente(Id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un errror en la funcion: ExisteCliente {ex.Message}");
                return false;

            }
        }

        public async Task<bool> ExisteNombreCliente(string Nombre)
        {
            try
            {
                bool result = await _clienteRpo.ExisteNombreCliente(Nombre);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un errror en la funcion: ExisteNombreCliente {ex.Message}");
                return false;

            }
        }

        public async Task<ICollection<CleinteDTOs>> GetCliente()
        {
            try
            {
                var ListaCliente = await _clienteRpo.GetClientes();
                List<CleinteDTOs> ClienteDTOs = ListaCliente.Select(c => new CleinteDTOs
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Telefono = c.Telefono,
                    FechaNacimiento= c.FechaNacimiento,
                    Estado=c.Estado
                }).ToList();  
                return ClienteDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un error de funcion: GetCliente en service {ex.Message}");
                return null;
            }
        }

        public async Task<CleinteDTOs> GetClienteById(int Id)
        {
            try
            {
                Cliente Cliente = await _clienteRpo.GetCleinteById(Id);
                if (Cliente == null)
                {
                    return null;
                }
                CleinteDTOs Resul = _mapper.Map<CleinteDTOs>(Cliente);
                return Resul;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hay un Error de funcion GetClienteById {ex.Message} ");
                return null;
            }
        }
    }
}
