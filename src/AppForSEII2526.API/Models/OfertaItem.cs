namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        [Key]
        public int idOferta { get; set; }

        [Required]
        public int idHerramienta { get; set; }

        [Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioFinal { get; set; }
    }
}
