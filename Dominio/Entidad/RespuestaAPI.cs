

using System.Net;

namespace Dominio.Entidad
{
    public class RespuestaAPI
    {
        public RespuestaAPI()
        {
            ErroMessages = new List<string>();
        }
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErroMessages { get; set; }
        public object Resul { get; set; }
    }
}
