

namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(alquilarid), nameof(herramientaid))]
    public class alquilarItem
    {
        public alquilarItem()
        {
            alquilar = new alquilar();
            herramienta = new Herramienta();
        }

        public alquilarItem(int cantidad, int alquilarid, int herramientaid, float precio, alquilar alquilar, 
            Herramienta herramienta)
        {
            this.cantidad = cantidad;
            this.alquilarid = alquilarid;
            this.herramientaid = herramientaid;
            this.precio = precio;
            this.alquilar = alquilar;
            this.herramienta = herramienta;
        }

        [Required]
        public int cantidad { get; set; }

        public int alquilarid { get; set; }

        public int herramientaid { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }


        // Relaciones
        public alquilar alquilar { get; set; }
        public Herramienta herramienta { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is alquilarItem item &&
                   cantidad == item.cantidad &&
                   alquilarid == item.alquilarid &&
                   herramientaid == item.herramientaid &&
                   precio == item.precio &&
                   EqualityComparer<alquilar>.Default.Equals(alquilar, item.alquilar) &&
                   EqualityComparer<Herramienta>.Default.Equals(herramienta, item.herramienta);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(cantidad, alquilarid, herramientaid, precio, alquilar, herramienta);
        }
    }


}
