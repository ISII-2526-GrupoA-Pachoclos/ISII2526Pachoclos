namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {
        // alquilaritem falta
        // compraitems falta
        // ofertaitems falta
        // itemsreparacion falta
        [Key]
        public int id { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string material { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }
        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        [Required]
        public string tiempoReparacion { get; set; }

    }
}
