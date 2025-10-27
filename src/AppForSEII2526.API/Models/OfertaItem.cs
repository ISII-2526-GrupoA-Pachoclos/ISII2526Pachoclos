namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(ofertaId), nameof(herramientaid))]
    public class OfertaItem
    {
        private int id;

        public int ofertaId { get; set; }

        public int herramientaid { get; set; }

        [Required, Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioFinal { get; set; }

        public OfertaItem()
        {
        }
       

        public OfertaItem(int id, int porcentaje, float precioFinal, Herramienta herramienta)
        {
            this.id = id;
            this.porcentaje = porcentaje;
            this.precioFinal = precioFinal;
            this.herramienta = herramienta;
        }

        // Relaciones
        public Oferta oferta { get; set; }
        public Herramienta herramienta { get; set; }

    }
}
