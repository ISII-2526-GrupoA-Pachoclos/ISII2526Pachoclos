

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

        public override bool Equals(object? obj)
        {
            return obj is Herramienta herramienta &&
                   id == herramienta.id &&
                   material == herramienta.material &&
                   nombre == herramienta.nombre &&
                   precio == herramienta.precio &&
                   tiempoReparacion == herramienta.tiempoReparacion &&
                   EqualityComparer<IList<ReparacionItem>>.Default.Equals(ReparacionItem, herramienta.ReparacionItem) &&
                   EqualityComparer<IList<OfertaItem>>.Default.Equals(OfertaItems, herramienta.OfertaItems) &&
                   EqualityComparer<IList<ComprarItem>>.Default.Equals(ComprarItems, herramienta.ComprarItems) &&
                   EqualityComparer<fabricante>.Default.Equals(fabricante, herramienta.fabricante) &&
                   EqualityComparer<IList<alquilarItem>>.Default.Equals(alquilarItems, herramienta.alquilarItems);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(material);
            hash.Add(nombre);
            hash.Add(precio);
            hash.Add(tiempoReparacion);
            hash.Add(ReparacionItem);
            hash.Add(OfertaItems);
            hash.Add(ComprarItems);
            hash.Add(fabricante);
            hash.Add(alquilarItems);
            return hash.ToHashCode();
        }
    }
}
