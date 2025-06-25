using Aplicación.DTOs;
using Aplicación.Servicios.Interfaz;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServe _clienteServer;
        public ClienteController(IClienteServe clienteServer)
        {
            _clienteServer = clienteServer;
        }

        [HttpGet(Name = "Cliente")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCliente()
        {
            try
            {
                ICollection<CleinteDTOs> Lista = await _clienteServer.GetCliente();
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hay un error en servidor {ex.Message}");
            }
        }

        [HttpGet("{ClienteId:int}", Name = "GetCleinteById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetClienteById(int ClienteId)
        {
            try
            {
                CleinteDTOs Result = await _clienteServer.GetClienteById(ClienteId);
                return Ok(Result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Hay un error en servidor {ex.Message} ");

            }
        }

        [HttpPost("PostCliente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostCliente(CrearCleinteDTOs crearClienteDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (crearClienteDTOs == null)
                {
                    return BadRequest(ModelState);
                }
                if (!await _clienteServer.ExisteNombreCliente(crearClienteDTOs.Nombre))
                {
                    ModelState.AddModelError("", $"Algo salio mal al guardar");
                    return StatusCode(404, ModelState);
                }
                var crearCliente = await _clienteServer.CrearCliente(crearClienteDTOs);
                if (!crearCliente)
                {
                    ModelState.AddModelError("", $"Algo salio mal guadar el registro{crearClienteDTOs.Nombre}");
                    return StatusCode(404, ModelState);
                }
                return Ok("se guardado exitosamente ");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Hay un error en servidor {ex.Message}");

            }
        }
        [HttpPatch("PutCliente/{ClienteId:int}", Name = "PatchCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutCliente(int ClienteId, CrearCleinteDTOs crearClienteDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (crearClienteDTOs == null || ClienteId == 0)
                {
                    return BadRequest(crearClienteDTOs);
                }
                if (!await _clienteServer.ActualCliente(crearClienteDTOs, ClienteId))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizar el cliente {crearClienteDTOs.Nombre}");
                    return StatusCode(500, ModelState);
                }
                return Ok("Se actualizado el cliente ");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Hay un error en servidor {ex.Message}");
            }
        }
        [HttpDelete("DeleteCliene/{ClienteId:int}", Name = "ElminarCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCategoria(int Id)
        {
            try
            {
                 await _clienteServer.EliminarCliente(Id);
                return Ok("Se elimino el categoria ");
            }
            catch (Exception ex) {
                return StatusCode(500, $"Hay un error en servidor {ex.Message}");
            }
        
        }
        }
}
