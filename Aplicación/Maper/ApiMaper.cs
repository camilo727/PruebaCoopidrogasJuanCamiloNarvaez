using Aplicación.DTOs;
using AutoMapper;
using Dominio.Entidad;


namespace Aplicación.Maper
{
    public class ApiMaper : Profile
    {
        public ApiMaper()
        {
            CreateMap<Usuario, UsuarioDTOs>().ReverseMap();

            CreateMap<Cliente, CrearCleinteDTOs>().ReverseMap();
            CreateMap<Cliente, CleinteDTOs>().ReverseMap();

        }
    }
}
