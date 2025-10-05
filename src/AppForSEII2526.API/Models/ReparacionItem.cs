namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(idHerramienta), nameof(idReparacion))]
    public class ReparacionItem
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser un número positivo mayor que 0.")]
        public int cantidad { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 4)]
        public string? descripcion { get; set; }

        public int idHerramienta { get; set; }

        public int idReparacion { get; set; }

        [Required, Range(0.01, float.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        // Relación
        public Reparacion Reparacion { get; set; }

        public Herramienta Herramienta { get; set; }
        }
}
