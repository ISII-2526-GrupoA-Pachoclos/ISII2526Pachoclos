namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {

        [Key]
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string material { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }
        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        public string tiempoReparacion { get; set; }


        // Relaciones

        // alquilarItems falta

        public IList<ReparacionItem> ReparacionItem { get; set; }

        public IList<OfertaItem> OfertaItems { get; set; }

        public IList<ComprarItem> ComprarItems { get; set; }

    }
}
