namespace AppForSEII2526.API.Models
{
    public class fabricante
    {
        [Key]
        public int id { get; set; }


        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }

        public IList<Herramienta> herramientas { get; set; }

    }
}
