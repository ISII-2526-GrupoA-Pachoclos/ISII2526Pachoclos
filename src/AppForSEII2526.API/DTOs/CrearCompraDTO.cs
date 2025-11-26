using System.ComponentModel;
using System.Security.Policy;

namespace AppForSEII2526.API.DTOs
{
    public class CrearCompraDTO
    {
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        [Required, StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        public string? correoElectronico { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "El número de teléfono no es válido. Ejemplo válido: 9876543210")]
        public string? numTelefono { get; set; }

        public string direccionEnvio { get; set; }

        public formaPago metodoPago { get; set; }

        public IList<CompraItemDTO> HerramientasCompradas { get; set; }

        
    }
}
