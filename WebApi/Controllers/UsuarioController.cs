using Aplicación.DTOs;
using Aplicación.Servicios.Interfaz;
using AutoMapper;
using Dominio.Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/UsuarioCotroller")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServer _usuarios;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;
        public UsuarioController(IUsuarioServer usuarioServer, IMapper mapper)
        {
            _usuarios=usuarioServer;
            _mapper=mapper;
            _respuestaAPI = new RespuestaAPI();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        //[ResponseCache(CacheProfileName = "PordeFecto30Segundo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                var ListaUsuario = await _usuarios.GetUsuarios();
                var usuarioDTOs = ListaUsuario.Select(u => _mapper.Map<UsuarioDTOs>(u)).ToList();

                return Ok(usuarioDTOs);
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
           
            
        }

        [AllowAnonymous]
        [HttpPost("registro")]
        //[ResponseCache(CacheProfileName = "PordeFecto30Segundo")]
        [ProducesResponseType(201, Type = typeof(UsuarioLoginRespuestaDTOs))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRegistre([FromBody] UsuarioRegistroDTOs usuarioRegistro)
        {
            try
            {
                bool ValidarNombre = await _usuarios.ValidadUsuario(usuarioRegistro.Nombre);
                if (!ValidarNombre)
                {
                    _respuestaAPI.statusCode = HttpStatusCode.BadRequest;
                    _respuestaAPI.IsSuccess = false;
                    _respuestaAPI.ErroMessages.Add("el nombre usuario ya existe");
                    return BadRequest(_respuestaAPI);
                }
                var usuario = await _usuarios.Registros(usuarioRegistro);
                if (usuario == null)
                {
                    _respuestaAPI.statusCode = HttpStatusCode.BadRequest;
                    _respuestaAPI.IsSuccess = false;
                    _respuestaAPI.ErroMessages.Add("Error en el registro");
                    return BadRequest(_respuestaAPI);
                }
                _respuestaAPI.statusCode = HttpStatusCode.OK;
                _respuestaAPI.IsSuccess = true;
                return Ok(_respuestaAPI);
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");

            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        //[ResponseCache(CacheProfileName = "PordeFecto30Segundo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTOs usuarioLoginDTOs)
        {
            var RespuestaLogin = await _usuarios.Login(usuarioLoginDTOs);
            if (RespuestaLogin.usuarios == null || string.IsNullOrEmpty(RespuestaLogin.Token))
            {
                _respuestaAPI.statusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErroMessages.Add("El nombre de usuario o password no son incorrecto");
                return BadRequest(_respuestaAPI);
            }
            _respuestaAPI.statusCode = HttpStatusCode.OK;
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Resul = RespuestaLogin;
            return Ok(_respuestaAPI);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{UsuarioId:int}", Name = "GetUsuarios")]
        //[ResponseCache(CacheProfileName = "PordeFecto30Segundo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< IActionResult> GetUsuariosById(int UsuarioId)
        {
            try
            {
                Usuario ItemUsuario = await _usuarios.GetUsuarioByUsuario(UsuarioId);

                if (ItemUsuario == null)
                {
                    return NotFound();
                }
                UsuarioDTOs usuarioDTOs = _mapper.Map<UsuarioDTOs>(ItemUsuario);
                return Ok(usuarioDTOs);
            }
            catch (Exception ex) {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            
        }
    }
}
