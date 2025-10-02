namespace AppForSEII2526.API.Models
{
    public class ComprarItem
    {
        public int cantidad { get; set; }

        public string descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        [Key]
        public int idCompra { get; set; }

        public int idHerramienta { get; set; }
    }
}
