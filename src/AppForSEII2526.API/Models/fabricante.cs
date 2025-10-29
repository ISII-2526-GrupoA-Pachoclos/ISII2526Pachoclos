namespace AppForSEII2526.API.Models
{
    public class fabricante
    {
        public fabricante()
        {
            herramientas = new List<Herramienta>();
        }

        public fabricante(int id, string nombre, IList<Herramienta> herramientas)
        {
            this.id = id;
            this.nombre = nombre;
            this.herramientas = herramientas;
        }

        [Key]
        public int id { get; set; }


        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }

        public IList<Herramienta> herramientas { get; set; }


    }
}
