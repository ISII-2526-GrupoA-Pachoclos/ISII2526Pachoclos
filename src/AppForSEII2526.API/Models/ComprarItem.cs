namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(compraId), nameof(herramientaid))]
    public class ComprarItem
    {
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
