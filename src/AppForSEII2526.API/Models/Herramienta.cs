namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {

        public Herramienta()
        {
            ReparacionItem = new List<ReparacionItem>();
            OfertaItems = new List<OfertaItem>();
            ComprarItems = new List<ComprarItem>();
            fabricante = new fabricante();
            alquilarItems = new List<alquilarItem>();
        }

        public Herramienta(int id, string material, string nombre, float precio, string tiempoReparacion,
            IList<ReparacionItem> reparacionItem, IList<OfertaItem> ofertaItems, IList<ComprarItem> comprarItems,
            fabricante fabricante, IList<alquilarItem> alquilarItems)
        {
            this.id = id;
            this.material = material;
            this.nombre = nombre;
            this.precio = precio;
            this.tiempoReparacion = tiempoReparacion;
            ReparacionItem = reparacionItem;
            OfertaItems = ofertaItems;
            ComprarItems = comprarItems;
            this.fabricante = fabricante;
            this.alquilarItems = alquilarItems;
        }

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


        public IList<ReparacionItem> ReparacionItem { get; set; }

        public IList<OfertaItem> OfertaItems { get; set; }

        public IList<ComprarItem> ComprarItems { get; set; }

        public fabricante fabricante { get; set; }

        public IList<alquilarItem> alquilarItems { get; set; }


    }
}
