using Aplicación.DTOs;
using Aplicación.Servicios.Interfaz;
using Dominio.Entidad;
using Infraestructura.Repositorio.Interfaz;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplicación.Servicios.Repositorio
{
    public class UsuarioServer: IUsuarioServer
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private static string _claveSecreta;
        public UsuarioServer(IUsuarioRepo usuarioRepo, IConfiguration configuration)
        {
            _usuarioRepo = usuarioRepo;
            _claveSecreta = configuration["ApiSettings:Secreta"];
        }

        public async Task<Usuario> GetUsuarioByUsuario(int Id)
        {
            try
            {
                return await _usuarioRepo.GetUsuarioByUsuario(Id);
            }
            catch (Exception ex) {
                Console.WriteLine($" se produce un error  En la funcion: GetUsuarioByUsuario {ex.Message}");
                return null;
            }
            
        }

        public async Task<ICollection<Usuario>> GetUsuarios()
        {
            try
            {
                return await _usuarioRepo.GetUsuarios();
            }
            catch (Exception ex) {
                Console.WriteLine($"se produce un error en la funcion: GetUsuarios {ex.Message}");
                return null;
            }
         
        }

        public async Task<UsuarioLoginRespuestaDTOs> Login(UsuarioLoginDTOs usuarioLoginDTOs)
        {
            try
            {

                Usuario usuario = new Usuario();
                usuario.NombreUsuario = usuarioLoginDTOs.NombreUsuario;
                usuario.Password = usuarioLoginDTOs.Password;
                Usuario usuarioRespuesta = new Usuario();
                usuarioRespuesta = await _usuarioRepo.Login(usuario);
                if (usuarioRespuesta == null)
                {
                    UsuarioLoginRespuestaDTOs Respueta = new UsuarioLoginRespuestaDTOs();
                    Respueta.Token = "";
                    Respueta.usuarios = null;
                    return Respueta;
                }
                var manejadoToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_claveSecreta);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, usuarioRespuesta.NombreUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioRespuesta.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = manejadoToken.CreateToken(tokenDescriptor);
                UsuarioLoginRespuestaDTOs usuarioLoginRespuesta = new UsuarioLoginRespuestaDTOs();
                usuarioLoginRespuesta.Token = manejadoToken.WriteToken(token);
                usuarioLoginRespuesta.usuarios = usuario;
                return usuarioLoginRespuesta;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"se produce un error en la funcion: Login {ex.Message}");
                return null;
            }
        }

        public async Task<Usuario> Registros(UsuarioRegistroDTOs UsuarioLoginDTOs)
        {
            try
            {
                Usuario Usuario = new Usuario();
                if (UsuarioLoginDTOs != null)
                {
                    Usuario.NombreUsuario = UsuarioLoginDTOs.NombreUsuario;
                    Usuario.Nombre = UsuarioLoginDTOs.Nombre;
                    Usuario.Role = UsuarioLoginDTOs.Role;
                    Usuario.Password = UsuarioLoginDTOs.Password;
                }
                Usuario UsuarioRespuesta = new Usuario();
                UsuarioRespuesta = await _usuarioRepo.Registros(Usuario);
                return UsuarioRespuesta;
            }
            catch (Exception ex) {

                Console.WriteLine($"se produce un error en la funcion: Registros {ex.Message }");
                return null;
            
            }
            


        }

        public async Task<bool> ValidadUsuario(string Usuario)
        {
            try
            {
                return await _usuarioRepo.ValidadUsuario(Usuario);
            }
            catch (Exception ex) {
                Console.WriteLine($"se produce un error en la fucion: ValidadUsuario {ex.Message}");
                return false;
            }
          
        }
    }
}
