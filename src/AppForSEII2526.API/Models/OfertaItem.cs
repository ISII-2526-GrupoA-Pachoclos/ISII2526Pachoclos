
namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(ofertaId), nameof(herramientaid))]
    public class OfertaItem
    {
        public OfertaItem()
        {
            oferta = new Oferta();
            herramienta = new Herramienta();
        }

        public OfertaItem(int ofertaId, int herramientaid, int porcentaje, float precioFinal, Oferta oferta, 
            Herramienta herramienta)
        {
            this.ofertaId = ofertaId;
            this.herramientaid = herramientaid;
            this.porcentaje = porcentaje;
            this.precioFinal = precioFinal;
            this.oferta = oferta;
            this.herramienta = herramienta;
        }

        public int ofertaId { get; set; }

        public int herramientaid { get; set; }

        [Required, Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioFinal { get; set; }

        // Relaciones
        public Oferta oferta { get; set; }
        public Herramienta herramienta { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItem item &&
                   ofertaId == item.ofertaId &&
                   herramientaid == item.herramientaid &&
                   porcentaje == item.porcentaje &&
                   precioFinal == item.precioFinal &&
                   EqualityComparer<Oferta>.Default.Equals(oferta, item.oferta) &&
                   EqualityComparer<Herramienta>.Default.Equals(herramienta, item.herramienta);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ofertaId, herramientaid, porcentaje, precioFinal, oferta, herramienta);
        }
    }
}
