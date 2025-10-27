
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

        public HerramientasParaRepararDTO()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaRepararDTO dTO &&
                   id == dTO.id &&
                   material == dTO.material &&
                   nombre == dTO.nombre &&
                   precio == dTO.precio &&
                   tiempoReparacion == dTO.tiempoReparacion &&
                   fabricante == dTO.fabricante;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, material, nombre, precio, tiempoReparacion, fabricante);
        }
    }
}
