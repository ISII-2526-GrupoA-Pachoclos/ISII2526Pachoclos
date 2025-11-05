using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.DTOs
{
    public class ReparacionItemDTO
    {
        public ReparacionItemDTO()
        {
        }

        public ReparacionItemDTO(int herramientaId, float precio, string nombreHerramienta, 
            string? descripcion, int cantidad, string? tiempoReparacion)
        {
            HerramientaId = herramientaId;
            this.precio = precio;
            this.nombreHerramienta = nombreHerramienta;
            this.descripcion = descripcion;
            this.cantidad = cantidad;
            this.tiempoReparacion = tiempoReparacion;
        }

        [Key]
        public int HerramientaId { get; set; }

        [Required, Range(0.001, float.MaxValue, ErrorMessage = "El precio debe ser un valor positivo mayor que 0.001.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombreHerramienta { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 4)]
        public string? descripcion { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo mayor que 0.")]
        public int cantidad { get; set; }

        public string? tiempoReparacion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO other &&
                   HerramientaId == other.HerramientaId &&
                   nombreHerramienta == other.nombreHerramienta &&
                   precio == other.precio &&
                   descripcion == other.descripcion &&
                   cantidad == other.cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, precio, descripcion, cantidad, nombreHerramienta);
        }

    }
}
