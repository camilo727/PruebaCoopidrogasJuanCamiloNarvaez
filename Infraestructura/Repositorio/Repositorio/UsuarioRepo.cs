
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidad;
using Infraestructura.Datos;
using Infraestructura.Repositorio.Interfaz;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;


namespace Infraestructura.Repositorio.Repositorio
{
    public class UsuarioRepo: IUsuarioRepo
    {
        private readonly AppDbContext _conexion;
        private string _claveSecreta;
        private readonly IMapper _mapper;
        public UsuarioRepo(AppDbContext Conexion,IMapper mapper,IConfiguration configuration )
        {
            _conexion = Conexion;
            _mapper = mapper;
            _claveSecreta = configuration["ApiSettings:Secreta"];
        }

        public async Task<Usuario> GetUsuarioByUsuario(int Id)
        {
            try
            {
                return await _conexion.usuarios.FirstOrDefaultAsync(a => a.Id == Id);
            }
            catch (Exception ex) {

                Console.WriteLine($"se produce un erro al ser la consulta por Id {Id} "+ex.Message);
                return null;
            }
            
        }

        public async Task<ICollection<Usuario>> GetUsuarios()
        {
            try
            {
                return await _conexion.usuarios.ToListAsync();
            }
            catch (Exception ex) { 
                 Console.WriteLine($" se produce un error  hacer consulta al usuario {ex.Message}");
                return null;
            }
            
        }

        public async Task<Usuario> Login(Usuario usuarioLogin)
        {
            try
            {

                var PasswordEncriptado = ObternerMd5(usuarioLogin.Password);
                Usuario usuario = new Usuario();
                usuario = await _conexion.usuarios.FirstOrDefaultAsync(U => U.NombreUsuario.ToLower() == usuarioLogin.NombreUsuario.ToLower()
                && U.Password == PasswordEncriptado);

                return usuario;
                
                

            }
            catch (Exception ex) {
                Console.WriteLine($"se produce un error al hacer la comparacion del login {ex.Message}");
                return null;
            }
            
        }

        public async Task<Usuario> Registros(Usuario UsuarioLogin)
        {
            try
            {
                var passwordEncriptado = ObternerMd5(UsuarioLogin.Password);
                Usuario usuario = new Usuario();

                usuario.Nombre = UsuarioLogin.Nombre;
                usuario.NombreUsuario = UsuarioLogin.NombreUsuario;
                usuario.Password = passwordEncriptado;
                usuario.Role = UsuarioLogin.Role;
                _conexion.Add(usuario);
                await _conexion.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex) {
                Console.WriteLine($"Se Produce un error al registro del usuario {ex.Message}");
                return null; 
            }
            
        }

        public async Task<bool> ValidadUsuario(string Usuario)
        {
            try
            {
                int Valida = await _conexion.usuarios.CountAsync(c => c.Nombre.ToLower() == Usuario.ToLower());
                if (Valida > 1)
                {
                    return  false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" hay un error en la valudacion del usuario {ex.Message}");
                return false;
            }
           
        }
        private static string ObternerMd5(string Password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(Password);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
