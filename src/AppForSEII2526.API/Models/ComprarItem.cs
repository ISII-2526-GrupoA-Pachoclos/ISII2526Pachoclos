namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(compraId), nameof(herramientaid))]
    public class ComprarItem
    {
        public ComprarItem()
        {
            compra = new Compra();
            herramienta = new Herramienta();
        }

        public ComprarItem(int cantidad, string descripcion, float precio, int compraId, int herramientaid, Compra compra,
            Herramienta herramienta)
        {
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.precio = precio;
            this.compraId = compraId;
            this.herramientaid = herramientaid;
            this.compra = compra;
            this.herramienta = herramienta;
        }

        [Required]
        public int cantidad { get; set; }

        [Required]
        public string descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        
        public int compraId { get; set; }

        public int herramientaid { get; set; }

        public Compra compra { get; set; }

        public Herramienta herramienta { get; set; }

        
        
        
    }
}
