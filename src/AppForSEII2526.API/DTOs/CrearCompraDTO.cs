using System.Security.Policy;

namespace AppForSEII2526.API.DTOs
{
    public class CrearCompraDTO
    {
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string direccionEnvio { get; set; }

        public formaPago metodoPago { get; set; }
        public IList<CompraItemDTO> HerramientasCompradas { get; set; }
    }
}
