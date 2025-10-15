namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaRepararDTO
    {
        [Key]
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string material { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        public string tiempoReparacion { get; set; }

        public string fabricante { get; set; }

        public HerramientasParaRepararDTO(int id, string material, string nombre, float precio, string tiempoReparacion, string fabricante)
        {
            this.id = id;
            this.material = material;
            this.nombre = nombre;
            this.precio = precio;
            this.tiempoReparacion = tiempoReparacion;
            this.fabricante = fabricante;
        }
    }
}
