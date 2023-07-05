using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace API_REST.Controllers.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string TipoUsuario { get; set; }

    }
}
