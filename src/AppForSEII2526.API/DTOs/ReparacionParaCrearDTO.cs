using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class ReparacionParaCrearDTO
    {
        public ReparacionParaCrearDTO()
        {
            Herramientas = new List<ReparacionItemDTO>();
        }

        public ReparacionParaCrearDTO(string nombreC, string apellidos, string? numTelefono, metodoPago metodoPago, 
            IList<ReparacionItemDTO> herramientas)
        {
            this.nombreC = nombreC;
            this.apellidos = apellidos;
            this.numTelefono = numTelefono;
            this.metodoPago = metodoPago;
            Herramientas = herramientas;
        }

        // Constructor para pruebas unitarias del POST
        public ReparacionParaCrearDTO(string nombreC, string apellidos, string? numTelefono, metodoPago metodoPago,
        DateTime fechaEntrega, IList<ReparacionItemDTO> herramientas)
        {
            this.nombreC = nombreC;
            this.apellidos = apellidos;
            this.numTelefono = numTelefono;
            this.metodoPago = metodoPago;
            this.fechaEntrega = fechaEntrega;
            Herramientas = herramientas;
        }

        [Required, StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string nombreC { get; set; }

        [Required, StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string apellidos { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "El número de teléfono no es válido. Ejemplo válido: 9876543210")]
        public string? numTelefono { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public metodoPago metodoPago { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [Required(ErrorMessage = "Debe incluir al menos una herramienta para reparar.")]
        public IList<ReparacionItemDTO> Herramientas { get; set; } = new List<ReparacionItemDTO>();




        public override bool Equals(object? obj)
        {
            return obj is ReparacionParaCrearDTO dTO &&
                   nombreC == dTO.nombreC &&
                   apellidos == dTO.apellidos &&
                   numTelefono == dTO.numTelefono &&
                   metodoPago == dTO.metodoPago &&
                   fechaEntrega == dTO.fechaEntrega &&
                   Herramientas.SequenceEqual(dTO.Herramientas);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombreC, apellidos, numTelefono, metodoPago, Herramientas);
        }
    }
}