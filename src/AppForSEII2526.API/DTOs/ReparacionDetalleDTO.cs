using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http.Features;

namespace AppForSEII2526.API.DTOs
{
    public class ReparacionDetalleDTO
    {
        public ReparacionDetalleDTO()
        {
        }

        public ReparacionDetalleDTO(string nombre, string apellido, DateTime fechaEntrega,
            DateTime fechaRecogida, float? precioTotal,
            IList<ReparacionItemDTO> herramientasAReparar)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.fechaEntrega = fechaEntrega;
            this.fechaRecogida = fechaRecogida;
            HerramientasAReparar = herramientasAReparar;
            this.precioTotal = HerramientasAReparar.Sum(i => i.precio * i.cantidad);
        }

        [Required, StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string nombre { get; set; }

        [Required, StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string apellido { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRecogida { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float? precioTotal { get; set; }

        public IList<ReparacionItemDTO> HerramientasAReparar { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionDetalleDTO other &&
                    nombre == other.nombre &&
                    apellido == other.apellido &&
                    fechaEntrega == other.fechaEntrega &&
                    precioTotal == other.precioTotal &&
                    HerramientasAReparar == other.HerramientasAReparar;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(nombre, apellido, fechaEntrega, precioTotal, HerramientasAReparar);
        }
    }
}
