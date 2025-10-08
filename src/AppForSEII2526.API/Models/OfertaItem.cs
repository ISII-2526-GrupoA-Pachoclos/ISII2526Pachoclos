namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(ofertaId), nameof(herramientaid))]
    public class OfertaItem
    {
        
        public int ofertaId { get; set; }

        public int herramientaid { get; set; }

        [Required, Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioFinal { get; set; }

        // Relaciones
        public Oferta oferta { get; set; }
        public Herramienta herramienta { get; set; }

    }
}
